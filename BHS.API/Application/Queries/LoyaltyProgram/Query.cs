using BHS.API.ViewModels;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.API.Application.Queries.LoyaltyProgram;

public class Query
{
    public static string GetAllLoyaltyProgram(string userId, QueryTemplate queryTemplate)
    {
        queryTemplate.PageIndex = queryTemplate.PageIndex < 1 ? 0 : queryTemplate.PageIndex - 1;
        return $@"SELECT l.Id, l.Name, l.ImgBannerUrl, l.StartDate, l.EndDate, l.ExpirationDate, l.Type,
                            CASE WHEN l.Type = {LoyaltyProgramType.Purchase.ToInt()} OR l.Type = {LoyaltyProgramType.QrCode.ToInt()} THEN N'Tích điểm'
                            WHEN l.Type = {LoyaltyProgramType.GiftExchange.ToInt()} THEN N'Đổi quà'
                            ELSE '' END as TypeName,
       (SELECT IIF(SUM(pu.Point) is not null, SUM(pu.Point), 0) FROM PointOfUser pu WITH (NOLOCK) 
                             WHERE pu.UserId = '{userId}'
                                AND pu.SourceId = l.Id
                                AND pu.ProgramType IN ({PointOfUserType.Qr.ToInt()}, {PointOfUserType.Order.ToInt()})) as PointOfUser,
                            v.*
                         FROM LoyaltyProgram l WITH (NOLOCK) INNER JOIN Vendor v  WITH (NOLOCK) ON l.VendorId = v.Id
                         WHERE l.ExpirationDate >= getutcdate() AND l.StartDate <= getutcdate()
                         AND l.Status = {CommonStatus.Active.ToInt()}
                         ORDER BY l.StartDate desc";
    }

    public static string GetLoyaltyProgramsByName(string userId, string name, QueryTemplate queryTemplate)
    {
        queryTemplate.PageIndex = queryTemplate.PageIndex < 1 ? 0 : queryTemplate.PageIndex - 1;
        return $@"SELECT l.Id, l.Name, l.ImgBannerUrl, l.StartDate, l.EndDate, l.ExpirationDate, l.Type,
                            CASE WHEN l.Type = {LoyaltyProgramType.Purchase.ToInt()} OR l.Type = {LoyaltyProgramType.QrCode.ToInt()} THEN N'Tích điểm'
                            WHEN l.Type = {LoyaltyProgramType.GiftExchange.ToInt()} THEN N'Đổi quà'
                            ELSE '' END as TypeName,
       (SELECT IIF(SUM(pu.Point) is not null, SUM(pu.Point), 0) FROM PointOfUser pu WITH (NOLOCK) 
                             WHERE pu.UserId = '{userId}'
                                AND pu.SourceId = l.Id
                                AND pu.ProgramType IN ({PointOfUserType.Qr.ToInt()}, {PointOfUserType.Order.ToInt()})) as PointOfUser,
                            v.*
                         FROM LoyaltyProgram l WITH (NOLOCK) INNER JOIN Vendor v  WITH (NOLOCK) ON l.VendorId = v.Id
                         WHERE l.ExpirationDate >= getutcdate() AND l.StartDate <= getutcdate()
                         AND l.Status = {CommonStatus.Active.ToInt()} AND l.Name LIKE N'%{name}%'
                         ORDER BY l.StartDate desc";
    }

    public static string GetAllGifts(int vendorId, QueryTemplate queryTemplate)
    {
        queryTemplate.PageIndex = queryTemplate.PageIndex < 1 ? 0 : queryTemplate.PageIndex - 1;
        return $@"SELECT l.Id, l.Name, l.ImgBannerUrl, l.StartDate, l.EndDate, l.ExpirationDate, l.Type,
                            CASE WHEN l.Type = {LoyaltyProgramType.Purchase.ToInt()} OR l.Type = {LoyaltyProgramType.QrCode.ToInt()} THEN N'Tích điểm'
                            WHEN l.Type = {LoyaltyProgramType.GiftExchange.ToInt()} THEN N'Đổi quà'
                            ELSE '' END as TypeName,      
                            i.*, p.*, pr.*, g.*, f.*
                         FROM LoyaltyProgram l WITH (NOLOCK)
                         LEFT JOIN LoyaltyProgramImage i  WITH (NOLOCK) ON l.Id = i.LoyaltyProgramId
                         LEFT JOIN ProductParticipatingLoyalty p WITH (NOLOCK) ON l.Id = p.LoyaltyProgramId
                         LEFT JOIN  Product pr WITH (NOLOCK) ON p.ProductId = pr.Id
                         LEFT JOIN GiftOfLoyalTy g WITH (NOLOCK) ON g.LoyaltyProgramId = l.Id
                         LEFT JOIN Fortune f WITH (NOLOCK) ON (g.SourceId = f.Id AND g.Type = {GiftType.RotationLuck.ToInt()})
                         WHERE l.VendorId = {vendorId} AND l.ExpirationDate >= getutcdate() AND l.StartDate <= getutcdate()
                         AND l.Status = {CommonStatus.Active.ToInt()} AND  l.Type = {LoyaltyProgramType.GiftExchange.ToInt()}
                         ORDER BY l.StartDate desc";
    }

    public static string GetLoyaltyProgram(string userId, int loyaltyProgramId)
    {
        return $@"SELECT l.Id, l.Name, l.ImgBannerUrl, l.StartDate, l.EndDate, l.ExpirationDate, l.Type,
                            CASE WHEN l.Type = {LoyaltyProgramType.Purchase.ToInt()} OR l.Type = {LoyaltyProgramType.QrCode.ToInt()} THEN N'Tích điểm'
                            WHEN l.Type = {LoyaltyProgramType.GiftExchange.ToInt()} THEN N'Đổi quà'
                            ELSE '' END as TypeName,      
                           v.*, (SELECT IIF(SUM(Point) is not null, SUM(Point), 0)
                      FROM PointOfUser p2
                            LEFT JOIN LoyaltyProgram l2 on (p2.ProgramType IN ({PointOfUserType.Qr.ToInt()}, {PointOfUserType.Order.ToInt()}) AND p2.SourceId = l2.Id AND l2.ExpirationDate > GETUTCDATE())
                      WHERE UserId = '{userId}'
                        AND p2.VendorId = l.VendorId
                        AND ProgramType <> {PointOfUserType.GiftExchange.ToInt()})
                         as TotalPoint, i.*, p.*, pr.*, g.*, f.*
                         FROM LoyaltyProgram l WITH (NOLOCK) INNER JOIN Vendor v  WITH (NOLOCK) ON l.VendorId = v.Id
                         LEFT JOIN LoyaltyProgramImage i  WITH (NOLOCK) ON l.Id = i.LoyaltyProgramId
                         LEFT JOIN ProductParticipatingLoyalty p WITH (NOLOCK) ON l.Id = p.LoyaltyProgramId
                         LEFT JOIN  Product pr WITH (NOLOCK) ON p.ProductId = pr.Id
                         LEFT JOIN GiftOfLoyalty g WITH (NOLOCK) ON g.LoyaltyProgramId = l.Id
                         LEFT JOIN Fortune f WITH (NOLOCK) ON (g.SourceId = f.Id AND g.Type = {GiftType.RotationLuck.ToInt()})
                         WHERE l.Id = {loyaltyProgramId}
                         ORDER BY l.StartDate desc";
    }
}