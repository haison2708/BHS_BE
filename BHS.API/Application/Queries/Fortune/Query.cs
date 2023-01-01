using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.API.Application.Queries.Fortune;

public static class Query
{
    public static string GetFortuneQuery(int fortuneId, string userId)
    {
        return $@"SELECT  f.Id, f.Descr, f.FromDate, f.ToDate, f.ImageBanner, 
                          (SELECT IIF(SUM(TurnAvailable) is not null, SUM(TurnAvailable), 0) FROM FortuneTurnOfUser WHERE FortuneId = f.Id AND UserId = '{userId}') 
                              as TurnsOfUser, d.*, v.*
                     FROM Fortune f WITH (NOLOCK) INNER JOIN Vendor v WITH (NOLOCK) on f.VendorId = v.Id
                     LEFT JOIN FortuneDetail d WITH (NOLOCK) on f.Id = d.FortuneId 
                     WHERE f.Id = {fortuneId}
                     AND f.Status = {CommonStatus.Active.ToInt()}";
    }

    public static string GetAllFortuneByVendorQuery(string userId, int vendorId)
    {
        return $@"SELECT f.Id, f.Descr, f.FromDate, f.ToDate, f.ImageBanner, 
                          (SELECT IIF(SUM(TurnAvailable) is not null, SUM(TurnAvailable), 0) FROM FortuneTurnOfUser WHERE FortuneId = f.Id AND UserId = '{userId}') 
                              as TurnsOfUser, d.*, v.*
                          FROM Fortune f WITH(NOLOCK)
                          INNER JOIN Vendor v WITH(NOLOCK) on f.VendorId = v.Id
                          LEFT JOIN FortuneDetail d WITH (NOLOCK) on f.Id = d.FortuneId
                          WHERE f.ToDate >= GETUTCDATE() AND f.FromDate <= GETUTCDATE() AND f.Status = {CommonStatus.Active.ToInt()} AND v.Id = {vendorId} 
                          ORDER BY f.FromDate desc";
    }
}