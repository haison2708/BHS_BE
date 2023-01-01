using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.API.Application.Queries.Vendor;

public static class Query
{
    public static string GetLuckyWheelTurnsQuery(string userId)
    {
        return $@"SELECT v.*, IIF(SUM(fu.TurnAvailable) is not null, SUM(fu.TurnAvailable), 0) as LuckyWheelTurns 
           FROM Vendor v WITH (NOLOCK) INNER JOIN Fortune f WITH (NOLOCK) on v.Id = f.VendorId
                 INNER JOIN FortuneTurnOfUser fu WITH (NOLOCK) on f.Id = fu.FortuneId
           WHERE v.Status = {CommonStatus.Active.ToInt()} AND v.IsDeleted = 0 AND f.ToDate > GETUTCDATE() AND f.FromDate < GETUTCDATE()
                  AND fu.UserId = '{userId}'
           GROUP BY v.Id, v.Name, Address, Email, Phone, ContactName, ContactPhone, ContactEmail, Website, Image, Logo, Info, v.Status,
                  IsDeleted, Fax, TaxCode, Rating, VendorKey, TotalFeedback, ShortName, v.CreatedAt";
    }

    public static string GetVendor(int vendorId, string userId)
    {
        return $@"SELECT DISTINCT v.*,
                        (SELECT UserId FROM UserFollowVendor WHERE VendorId = v.Id AND UserId = '{userId}' AND IsFollow = 1 GROUP BY UserId) AS UserId
           FROM Vendor v WITH (NOLOCK)
           WHERE v.Id = {vendorId}";
    }

    public static string GetVendorsByName(string userId, string vendorName, bool byUser)
    {
        var joinUser = string.Empty;
        if (byUser) joinUser = $@"AND u.UserId = '{userId}' AND u.IsFollow = 1";

        return
            $@"SELECT DISTINCT v.*, (SELECT USERID FROM UserFollowVendor WHERE VendorId = v.Id AND UserId = '{userId}' 
                        AND IsFollow = 1 GROUP BY UserId) AS UserId FROM Vendor v WITH (NOLOCK) LEFT JOIN UserFollowVendor u WITH (NOLOCK) ON u.VendorId = v.Id 
                             WHERE v.Name LIKE N'%{vendorName}%' AND v.Status = {CommonStatus.Active.ToInt()} {joinUser}";
    }

    public static string GetVendorsThatUserFollows(string userId)
    {
        return
            $@"SELECT v.*, u.UserId FROM Vendor v WITH (NOLOCK) INNER JOIN UserFollowVendor u WITH (NOLOCK) ON v.Id = u.VendorId
                                                     WHERE u.UserId = '{userId}' AND v.Status = {CommonStatus.Active.ToInt()} AND u.IsFollow = 1";
    }

    public static string GetAllVendor(string userId)
    {
        return $@"SELECT DISTINCT v.*,
                                (SELECT USERID FROM UserFollowVendor WITH (NOLOCK) WHERE VendorId = v.Id AND UserId = '{userId}' AND IsFollow = 1 GROUP BY UserId) AS UserId
                                FROM Vendor v WITH (NOLOCK)
                                WHERE v.Status = {CommonStatus.Active.ToInt()}";
    }

    public static string GetConfigRankOfVendor(int vendorId)
    {
        return $@"select *
                              from ConfigRankOfVendor with (nolock)
                              where VendorId = {vendorId}";
    }
}