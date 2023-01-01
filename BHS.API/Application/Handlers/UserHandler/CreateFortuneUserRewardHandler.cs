using System.Data;
using BHS.API.Application.Commands.UserCommand;
using BHS.API.Services;
using BHS.API.ViewModels.Fortunes;
using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;

namespace BHS.API.Application.Handlers.UserHandler;

public class CreateFortuneUserRewardHandler : IRequestHandler<CreateFortuneUserReward, FortuneDetailViewModel?>
{
    private readonly IConfiguration _configuration;
    private readonly IIdentityService _identityService;

    public CreateFortuneUserRewardHandler(IIdentityService identityService, IConfiguration configuration)
    {
        _identityService = identityService;
        _configuration = configuration;
    }

    public async Task<FortuneDetailViewModel?> Handle(CreateFortuneUserReward request,
        CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        try
        {
            /* Gọi stored procedure để trả quà cho User */
            await using var connection = new SqlConnection(_configuration["ConnectionString"]);
            var result = (await connection.QueryAsync<FortuneDetailViewModel>("CreateFortuneUserReward",
                new { userId, fortuneId = request.FortuneId },
                commandType: CommandType.StoredProcedure)).FirstOrDefault();
            return result;
        }
        catch
        {
            return null;
        }
    }
}