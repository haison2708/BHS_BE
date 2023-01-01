using System.Data;
using BHS.API.Application.Commands.NotifyCommand;
using BHS.API.Application.Commands.UserCommand;
using BHS.API.Services;
using BHS.Domain.Entities.LoyaltyPrograms;
using BHS.Domain.Enumerate;
using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;

namespace BHS.API.Application.Handlers.UserHandler;

public class GiftExchangeHandler : IRequestHandler<GiftExchange, string?>
{
    private readonly IConfiguration _configuration;
    private readonly IIdentityService _identityService;
    private readonly IMediator _mediator;

    public GiftExchangeHandler(IIdentityService identityService, IMediator mediator, IConfiguration configuration)
    {
        _identityService = identityService;
        _mediator = mediator;
        _configuration = configuration;
    }

    public async Task<string?> Handle(GiftExchange request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        try
        {
            /* Gọi stored procedure để trừ điểm, cộng quà cho User */
            await using var connection = new SqlConnection(_configuration["ConnectionString"]);
            var result = (await connection.QueryAsync<GiftOfLoyalty, LoyaltyProgram, GiftOfLoyalty>("GiftExchange",
                (g, l) =>
                {
                    g.LoyaltyProgram = l;
                    return g;
                }, new { userId, giftId = request.GiftOfLoyaltyId, quantity = request.Quantity },
                commandType: CommandType.StoredProcedure)).FirstOrDefault();
            if (result?.LoyaltyProgram is null)
                return "";
            /* Tạo thông báo */
            var newNotify = new CreateNotificationSetUp
            {
                Title = $"-{result.Point * request.Quantity} điểm tích lũy",
                SubTitle = DateTime.UtcNow.ToString(FormatDate.FormatDateDdMmYyyy),
                Type = NotifyType.PointsLoyalty,
                TimeStart = DateTimeOffset.UtcNow,
                Content = $"Đổi {result.Name} từ {result.LoyaltyProgram.Name}",
                VendorId = result.LoyaltyProgram.VendorId,
                Remark = "/my-gift",
                ToCurrentUser = true
            };
            /* Gọi tới CreateNotificationSetUpHandler */
            await _mediator.Send(newNotify, cancellationToken);
            return null;
        }
        catch (SqlException ex)
        {
            return ex.Message;
        }
    }
}