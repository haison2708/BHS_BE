using BHS.API.Application.Commands.UserCommand;
using BHS.API.Services;
using BHS.Domain.Entities.Users;
using BHS.Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BHS.API.Application.Handlers.UserHandler;

public class LogoutRequestHandler : IRequestHandler<LogoutRequest, bool>
{
    private readonly IConfiguration _configuration;
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public LogoutRequestHandler(IConfiguration configuration, IUnitOfWork unitOfWork, IIdentityService identityService)
    {
        _configuration = configuration;
        _unitOfWork = unitOfWork;
        _identityService = identityService;
    }

    public async Task<bool> Handle(LogoutRequest request, CancellationToken cancellationToken)
    {
        try
        {
            /*
             * Nếu TokenFcm = null thì sẽ xóa tất cả tokenFcm của User hiện tại
             * Ngược lại xóa tokenFcm được gửi lên
             */
            var userAppToken = await _unitOfWork.Repository<UserAppToken>().Get().Where(x => request.TokenFcm != null
                ? x.Token == request.Token
                : x.UserId == _identityService.GetUserIdentity()).ToListAsync(cancellationToken);
            _unitOfWork.Repository<UserAppToken>().DeleteRange(userAppToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            /*
             * Gọi lên IdentityServer để thu hồi token
             * Nếu client gửi lên Token = RefreshToken thì sẽ thu hồi tất cả RefreshToken và AccessToken của User
             * Ngược lại thu hồi AccessToken được gửi lên
             * (Cơ chế của IdentityServer4)
             */
            using var httpClient = new HttpClient();
            var revokeToken = new List<KeyValuePair<string, string>>
            {
                new("token", request.Token!),
                new("client_id", "ro.client"),
                new("client_secret", "secret")
            };
            var reqRevokeToken =
                new HttpRequestMessage(HttpMethod.Post, $"{_configuration["identityUrl"]}/connect/revocation")
                    { Content = new FormUrlEncodedContent(revokeToken) };

            var resRevokeToken = await httpClient.SendAsync(reqRevokeToken, cancellationToken);
            return resRevokeToken.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}