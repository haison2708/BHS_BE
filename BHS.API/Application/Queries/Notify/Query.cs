using BHS.API.ViewModels;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.API.Application.Queries.Notify;

public class Query
{
    public static string GetAllNotifyMessage(string userId, int vendorId, NotifyType type, QueryTemplate queryTemplate)
    {
        queryTemplate.PageIndex = queryTemplate.PageIndex < 1 ? 0 : queryTemplate.PageIndex - 1;
        var pagingSql = queryTemplate.PageSize < 1
            ? ""
            : $@" OFFSET {queryTemplate.PageSize * queryTemplate.PageIndex} ROWS FETCH NEXT {queryTemplate.PageSize} ROWS ONLY";

        var filter = Enum.IsDefined(typeof(NotifyType), type)
            ? type is NotifyType.PointsLoyalty or NotifyType.Loyalty
                ? $@" AND n.Type IN ({NotifyType.PointsLoyalty.ToInt()}, {NotifyType.Loyalty.ToInt()})"
                : $@" AND n.Type = {type.ToInt()}"
            : "";
        return $@"SELECT *
                          FROM NotifyMessage m WITH (NOLOCK) INNER JOIN NotificationSetup n on n.Id = m.NotificationSetUpId
                          WHERE m.IsShow = 1 AND m.UserId = '{userId}' AND n.Status = {CommonStatus.Active.ToInt()}
                          AND (n.VendorId is null OR n.VendorId = {vendorId}) {filter}
                          ORDER BY n.DatetimeStart desc {pagingSql};
                          SELECT COUNT(*)
                          FROM NotifyMessage m WITH (NOLOCK) INNER JOIN NotificationSetup n on n.Id = m.NotificationSetUpId
                          WHERE m.IsShow = 1 AND m.UserId = '{userId}' AND n.Status = {CommonStatus.Active.ToInt()} 
                          AND (n.VendorId is null OR n.VendorId = {vendorId}) {filter};
                          SELECT COUNT(*) as TotalNotSeen FROM NotifyMessage m INNER JOIN NotificationSetup n on n.Id = m.NotificationSetUpId
                          WHERE m.IsShow = 1 AND m.UserId = '{userId}' AND n.Status = {CommonStatus.Active.ToInt()}
                          AND (n.VendorId is null OR n.VendorId = {vendorId}) AND m.Seen = 0 {filter}";
    }
}