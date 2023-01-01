using BHS.API.Application.Commands.UserCommand;
using BHS.API.Services;
using BHS.API.ViewModels.Users;
using BHS.Domain.Entities.Users;
using BHS.Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BHS.API.Application.Handlers.UserHandler;

public class CreateUserAppTokenHandler : IRequestHandler<CreateUserAppToken, UserAppTokenViewModel>
{
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserAppTokenHandler(IUnitOfWork unitOfWork, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _identityService = identityService;
    }

    public async Task<UserAppTokenViewModel> Handle(CreateUserAppToken request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Repository<UserAppToken>().Get()
            .FirstOrDefaultAsync(x => x.Token == request.Token, cancellationToken);
        var userId = _identityService.GetUserIdentity();
        /* Kiểm tra token đã có trong UserAppToken hay chưa, nếu chưa thì thêm vào, ngược lại cập nhật UserId = UserId hiện tại */
        if (result is null)
        {
            var userAppToken = new UserAppToken
            {
                UserId = userId,
                AppId = request.AppId,
                Token = request.Token,
                Environment = request.Environment
            };
            result = await _unitOfWork.Repository<UserAppToken>().InsertAsync(userAppToken);
        }
        else
        {
            result.UserId = userId;
            result.CreatedAt = DateTime.UtcNow;
        }

        return (await _unitOfWork.SaveChangesAsync(cancellationToken)
            ? new UserAppTokenViewModel
                { Id = result.Id, UserId = result.UserId, AppId = result.AppId, Token = result.Token }
            : null)!;
    }
}