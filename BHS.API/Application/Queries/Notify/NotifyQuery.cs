using System.Data.SqlClient;
using BHS.API.Services;
using BHS.API.ViewModels;
using BHS.API.ViewModels.Notify;
using BHS.Domain.Enumerate;
using Dapper;

namespace BHS.API.Application.Queries.Notify;

public class NotifyQuery : BaseQuery, INotifyQuery
{
    public NotifyQuery(IConfiguration configuration, IIdentityService identityService) : base(configuration,
        identityService)
    {
    }

    public async Task<object> GetAllNotifyMessageAsync(NotifyType type, QueryTemplate queryTemplate)
    {
        var sql = Query.GetAllNotifyMessage(IdentityService.GetUserIdentity(), IdentityService.GetCurrentVendorId(),
            type, queryTemplate);
        await using var connection = new SqlConnection(ConnectionString);
        var query = await connection.QueryMultipleAsync(sql);

        var notifyMessages = query.Read<NotifyMessageViewModel, NotificationSetupViewModel, NotifyMessageViewModel>(
            (m, n) =>
            {
                m.NotificationSetup = n;
                return m;
            }).AsList();
        var count = query.ReadFirstOrDefault<long>();
        var countNotSeen = query.ReadFirstOrDefault<int>();
        return new PaginatedItemsViewModel<object>(queryTemplate.PageIndex, queryTemplate.PageSize, count,
            new[] { new { notifyMessages, NotSeen = countNotSeen } });
    }
}