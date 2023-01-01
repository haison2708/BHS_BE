using BHS.API.ViewModels;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.API.Application.Queries.User;

public static class Query
{
    public static string GetHistoriesPoints(string userId, int vendorId, QueryTemplate queryTemplate)
    {
        queryTemplate.PageIndex = queryTemplate.PageIndex < 1 ? 0 : queryTemplate.PageIndex - 1;
        var pagingSql = queryTemplate.PageSize < 1
            ? ""
            : $@" OFFSET {queryTemplate.PageSize * queryTemplate.PageIndex} ROWS FETCH NEXT {queryTemplate.PageSize} ROWS ONLY";
        return
            $@"SELECT DISTINCT p.Id, CASE WHEN p.ProgramType = {PointOfUserType.Qr.ToInt()} THEN CONCAT(N'Quét QrCode:', ' ', pro.Name)
                        WHEN p.ProgramType = {PointOfUserType.Order.ToInt()} THEN N'{PointOfUserType.Order.GetStringValue()}'
                        WHEN p.Type = {PointOfUserType.Expired.ToInt()} THEN N'{PointOfUserType.Expired.GetStringValue()}'
                        WHEN p.ProgramType = {PointOfUserType.GiftExchange.ToInt()} THEN CONCAT(N'Đổi', ' ', gl.Name) 
                        WHEN p.ProgramType = {PointOfUserType.RotationLuck.ToInt()} THEN N'{PointOfUserType.RotationLuck.ToInt()}'
                        ELSE N'Khác' END as Title, 
                        CASE WHEN f.Id IS NOT NULL THEN f.Descr
                        WHEN l.Id IS NOT NULL THEN l.Name
                        ELSE N'Khác' END ProgramName,
                        p.Point, CASE WHEN l.Id IS NOT NULL THEN l.ExpirationDate
                        ELSE '' END as ExpirationDate, p.CreatedAt
                        FROM PointOfUser p WITH (NOLOCK)
                        LEFT JOIN ProductParticipatingLoyalty pr WITH (NOLOCK) ON (p.ProgramType = {PointOfUserType.Qr.ToInt()} AND p.SourceDetailId = pr.Id)
                        LEFT JOIN Product pro WITH (NOLOCK) ON pr.ProductId = pro.Id
                        LEFT JOIN LoyaltyProgram l WITH (NOLOCK) ON (p.ProgramType IN ({PointOfUserType.Qr.ToInt()}, {PointOfUserType.Order.ToInt()}, {PointOfUserType.GiftExchange.ToInt()}) AND l.Id = p.SourceId)
                        LEFT JOIN GiftOfLoyalty gl WITH (NOLOCK) ON (p.ProgramType = {PointOfUserType.GiftExchange.ToInt()} AND gl.Id = p.SourceDetailId)
                        LEFT JOIN GiftOfUser gu WITH (NOLOCK) ON (gu.GiftOfLoyaltyId = gl.Id AND gu.UserId = p.UserId)
                        LEFT JOIN Fortune f WITH (NOLOCK) ON (p.ProgramType = {PointOfUserType.RotationLuck.ToInt()} AND p.SourceId = f.Id)
                        WHERE p.UserId = '{userId}' AND p.VendorId = {vendorId} AND 
                        ((p.Type = {PointOfUserType.Deducted.ToInt()} AND p.ProgramType = {PointOfUserType.GiftExchange.ToInt()}) OR (p.Type <> {PointOfUserType.Deducted.ToInt()}))
                        ORDER BY p.CreatedAt desc
                        {pagingSql};
                        SELECT COUNT(p.Id)
                        FROM PointOfUser p WITH (NOLOCK)
                        WHERE p.UserId = '{userId}' AND p.VendorId = {vendorId} AND 
                        ((p.Type = {PointOfUserType.Deducted.ToInt()} AND p.ProgramType = {PointOfUserType.GiftExchange.ToInt()}) OR (p.Type <> {PointOfUserType.Deducted.ToInt()}))";
    }

    public static string GetTotalPointOfPrograms(string userId, int vendorId, QueryTemplate queryTemplate)
    {
        queryTemplate.PageIndex = queryTemplate.PageIndex < 1 ? 0 : queryTemplate.PageIndex - 1;
        return $@"SELECT p.SourceId as ProgramId, 
                        CASE WHEN f.Id IS NOT NULL THEN f.Descr
                        WHEN l.Id IS NOT NULL THEN l.Name
                        ELSE N'Khác' END ProgramName,
                        IIF(SUM(p.Point) is not null, SUM(p.Point), 0) as TotalPoints, CASE WHEN l.Id IS NOT NULL THEN l.ExpirationDate
                        ELSE '' END as ExpirationDate
                        FROM PointOfUser p WITH (NOLOCK)
                        LEFT JOIN ProductParticipatingLoyalty pr WITH (NOLOCK) ON (p.ProgramType = {PointOfUserType.Qr.ToInt()} AND p.SourceDetailId = pr.Id)
                        LEFT JOIN Product pro WITH (NOLOCK) ON pr.ProductId = pro.Id
                        LEFT JOIN LoyaltyProgram l WITH (NOLOCK) ON (p.ProgramType IN ({PointOfUserType.Qr.ToInt()}, {PointOfUserType.Order.ToInt()}, {PointOfUserType.GiftExchange.ToInt()}) AND l.Id = p.SourceId)
                        LEFT JOIN GiftOfLoyalty gl WITH (NOLOCK) ON (p.ProgramType = {PointOfUserType.GiftExchange.ToInt()} AND gl.Id = p.SourceDetailId)
                        LEFT JOIN Fortune f WITH (NOLOCK) ON (p.ProgramType = {PointOfUserType.RotationLuck.ToInt()} AND p.SourceId = f.Id)
                        WHERE p.UserId = '{userId}' AND p.VendorId = {vendorId}
                        GROUP BY p.SourceId, f.Id, l.Id, f.Descr, l.Name, f.ToDate, l.ExpirationDate
                        HAVING IIF(SUM(p.Point) is not null, SUM(p.Point), 0) > 0
                        ORDER BY CASE WHEN f.Id is not null THEN f.ToDate ELSE l.ExpirationDate END DESC";
    }

    public static string GetCarts(string userId)
    {
        return $@"SELECT c.Id as CartId, c.UserId, c.Quantity, p.*, v.*
                             FROM Cart c WITH (NOLOCK) INNER JOIN Product p WITH (NOLOCK) ON c.ProductId = p.Id  
                             INNER JOIN Vendor v WITH (NOLOCK) ON c.VendorId = v.Id 
                             WHERE c.UserId = '{userId}'";
    }

    public static string GetGifts(string userId, int vendorId, int type, QueryTemplate queryTemplate)
    {
        queryTemplate.PageIndex = queryTemplate.PageIndex < 1 ? 0 : queryTemplate.PageIndex - 1;
        var pagingSql = queryTemplate.PageSize < 1
            ? ""
            : $@"OFFSET {queryTemplate.PageSize * queryTemplate.PageIndex} ROWS FETCH NEXT {queryTemplate.PageSize} ROWS ONLY";
        var filter = type switch
        {
            2 => @" AND gu.IsUsed = 1", // Đã dùng
            3 => @" AND gu.ExpirationDate < GETUTCDATE()", // Hết hạn
            _ => @" AND gu.IsUsed = 0 AND gu.ExpirationDate > GETUTCDATE()" // Còn hiệu lực
        };
        return $@"SELECT gu.*, gl.*, f.*, p.*
                             FROM GiftOfUser gu WITH (NOLOCK) INNER JOIN GiftOfLoyalty gl WITH (NOLOCK) ON gl.Id = gu.GiftOfLoyaltyId
                             LEFT JOIN Fortune f WITH (NOLOCK) ON (f.Id = gl.SourceId AND gl.Type = {GiftType.RotationLuck.ToInt()})
                             LEFT JOIN Product p WITH (NOLOCK) ON (p.Id = gl.SourceId AND gl.Type = {GiftType.Product.ToInt()})
                             WHERE gu.UserId = '{userId}'  AND gu.VendorId = {vendorId} {filter}
                             ORDER BY gu.CreatedAt desc {pagingSql};
                             SELECT COUNT(gu.Id)
                             FROM GiftOfUser gu WITH (NOLOCK) INNER JOIN GiftOfLoyalty gl WITH (NOLOCK) ON gl.Id = gu.GiftOfLoyaltyId
                             LEFT JOIN Fortune f WITH (NOLOCK) ON (gl.SourceId = f.Id AND gl.Type = {GiftType.RotationLuck.ToInt()})
                             LEFT JOIN Product p WITH (NOLOCK) ON (gl.SourceId = p.Id AND gl.Type = {GiftType.Product.ToInt()})
                             WHERE gu.UserId = '{userId}'  AND gu.VendorId = {vendorId} {filter}";
    }

    public static string GetGift(string userId, int vendorId, int giftId)
    {
        return $@"SELECT gu.*, gl.*, f.*, p.*, l.*
                             FROM GiftOfUser gu WITH (NOLOCK) INNER JOIN GiftOfLoyalty gl WITH (NOLOCK) ON gl.Id = gu.GiftOfLoyaltyId
                             INNER JOIN LoyaltyProgram l  WITH (NOLOCK) ON l.Id = gl.LoyaltyProgramId
                             LEFT JOIN Fortune f WITH (NOLOCK) ON (gl.SourceId = f.Id AND gl.Type = {GiftType.RotationLuck.ToInt()})
                             LEFT JOIN Product p WITH (NOLOCK) ON (gl.SourceId = p.Id AND gl.Type = {GiftType.Product.ToInt()})
                             WHERE gu.UserId = '{userId}'  AND gu.VendorId = {vendorId}
                                    AND gu.Id = {giftId}";
    }

    public static string GetHistoriesFortune(string userId, int fortuneId, QueryTemplate queryTemplate)
    {
        queryTemplate.PageIndex = queryTemplate.PageIndex < 1 ? 0 : queryTemplate.PageIndex - 1;
        var pagingSql = queryTemplate.PageSize < 1
            ? ""
            : $@" OFFSET {queryTemplate.PageSize * queryTemplate.PageIndex} ROWS FETCH NEXT {queryTemplate.PageSize} ROWS ONLY";
        return $@"SELECT f.Id, f.Descr, f.FromDate, f.ToDate, f.ImageBanner, 
                          (SELECT IIF(SUM(TurnAvailable) is not null, SUM(TurnAvailable), 0) FROM FortuneTurnOfUser WHERE FortuneId = f.Id AND UserId = '{userId}') as TurnsOfUser, fr.*, fd.*
                         FROM Fortune f WITH (NOLOCK) INNER JOIN FortuneUserReward fr WITH (NOLOCK) ON f.Id = fr.FortuneId
                         INNER JOIN FortuneDetail fd WITH (NOLOCK) ON fr.FortuneDetailId = fd.Id
                         WHERE fr.UserId = '{userId}' AND f.Id = {fortuneId} 
                         ORDER BY fr.CreatedAt desc
                         {pagingSql}
                         SELECT COUNT(DISTINCT fr.Id)
                         FROM Fortune f WITH (NOLOCK) INNER JOIN FortuneUserReward fr WITH (NOLOCK) ON f.Id = fr.FortuneId
                         INNER JOIN FortuneDetail fd WITH (NOLOCK) ON fr.FortuneDetailId = fd.Id
                         WHERE fr.UserId = '{userId}' AND f.Id = {fortuneId}";
    }

    public static string GetVendorOverview(string userId)
    {
        return
            $@"SELECT v.*, (SELECT COUNT(*) FROM GiftOfUser WHERE VendorId = v.Id AND IsUsed = 0 AND ExpirationDate > GETUTCDATE() AND UserId = '{userId}') as TotalGift, 
                            (SELECT IIF(SUM(Point) is not null, SUM(Point), 0)
                      FROM PointOfUser p2
                            LEFT JOIN LoyaltyProgram l2 on (p2.ProgramType IN ({PointOfUserType.Qr.ToInt()}, {PointOfUserType.Order.ToInt()}) AND p2.SourceId = l2.Id AND l2.ExpirationDate > GETUTCDATE())
                      WHERE UserId = '{userId}'
                        AND p2.VendorId = v.Id
                        AND ProgramType <> {PointOfUserType.GiftExchange.ToInt()}) as TotalPoint
                             ,r.*, l.*,
                            CASE WHEN l.Type = {LoyaltyProgramType.Purchase.ToInt()} OR l.Type = {LoyaltyProgramType.QrCode.ToInt()} THEN N'Tích điểm'
                            WHEN l.Type = {LoyaltyProgramType.GiftExchange.ToInt()} THEN N'Đổi quà'
                            ELSE '' END as TypeName, 
                            (SELECT IIF(SUM(pu.Point) is not null, SUM(pu.Point), 0) FROM PointOfUser pu WITH (NOLOCK) 
                             WHERE pu.UserId = '{userId}'
                                AND pu.SourceId = l.Id
                                AND pu.ProgramType IN ({PointOfUserType.Qr.ToInt()}, {PointOfUserType.Order.ToInt()})) as PointOfUser, p.*
                        FROM Vendor v WITH (NOLOCK) LEFT JOIN ConfigRankOfVendor r WITH (NOLOCK) ON r.VendorId = v.Id
                        LEFT JOIN (SELECT TOP 1 p1.VendorId, IIF(SUM(Point) is not null, SUM(Point), 0) as Point,  l1.ExpirationDate
                              FROM PointOfUser p1 WITH (NOLOCK)
                                INNER JOIN LoyaltyProgram l1 WITH (NOLOCK) ON (p1.ProgramType IN ({PointOfUserType.Qr.ToInt()}, {PointOfUserType.Order.ToInt()}) AND p1.SourceId = l1.Id)
                              WHERE UserId = '{userId}' AND l1.ExpirationDate > GETUTCDATE()
                              GROUP BY p1.VendorId, l1.ExpirationDate
                              HAVING IIF(SUM(Point) is not null, SUM(Point), 0) > 0
                              ORDER BY l1.ExpirationDate) p on (v.Id = p.VendorId)
                        LEFT JOIN LoyaltyProgram l WITH (NOLOCK) ON (l.VendorId = v.Id AND l.ExpirationDate >= getutcdate() AND l.StartDate <= getutcdate() AND l.Status = {CommonStatus.Active.ToInt()})
                        GROUP BY v.Id, v.Name, Address, Email, Phone, ContactName, ContactPhone, ContactEmail, Website, Image, Logo, Info, v.Status,
                                IsDeleted, Fax, TaxCode, Rating, VendorKey, TotalFeedback, ShortName, v.CreatedAt, r.Id, r.VendorId, Points, r.Name, r.CreatedAt, l.Id, l.ImgBannerUrl, l.StartDate, l.EndDate, l.ExpirationDate, l.Type, l.Name, l.VendorId, l.Status, l.CreatedAt,
                                p.VendorId, p.Point, p.ExpirationDate
                        ORDER BY l.StartDate desc";
    }

    public static string GetTotalPointsAndGifts(string userId, int vendorId)
    {
        return
            $@"SELECT (SELECT COUNT(*) FROM GiftOfUser WHERE VendorId = v.Id AND IsUsed = 0 AND ExpirationDate > GETUTCDATE() AND UserId = '{userId}') as TotalGift,
                            (SELECT IIF(SUM(Point) is not null, SUM(Point), 0)
                      FROM PointOfUser p2
                            LEFT JOIN LoyaltyProgram l2 on (p2.ProgramType IN ({PointOfUserType.Qr.ToInt()}, {PointOfUserType.Order.ToInt()}) AND p2.SourceId = l2.Id AND l2.ExpirationDate > GETUTCDATE())
                      WHERE UserId = '{userId}'
                        AND p2.VendorId = {vendorId}
                        AND ProgramType <> {PointOfUserType.GiftExchange.ToInt()}) as TotalPoint,
                                 (SELECT IIF(SUM(fu.TurnAvailable) is not null, SUM(fu.TurnAvailable), 0) FROM Vendor v WITH (NOLOCK) INNER JOIN Fortune f WITH (NOLOCK) on v.Id = f.VendorId
                                        INNER JOIN FortuneTurnOfUser fu WITH (NOLOCK) on f.Id = fu.FortuneId
                                    WHERE v.Status = {CommonStatus.Active.ToInt()} AND v.IsDeleted = 0 AND f.ToDate > GETUTCDATE() AND f.FromDate < GETUTCDATE() AND 
                                        fu.UserId = '{userId}') as LuckyWheelTurns
                             ,r.*, p.*
               FROM Vendor v WITH (NOLOCK) LEFT JOIN (SELECT TOP 1 Id, VendorId, Points, Name FROM ConfigRankOfVendor c WITH (NOLOCK)
                                                      WHERE Points <= (SELECT IIF(SUM(Point) is not null, SUM(Point), 0) FROM PointOfUser WITH (NOLOCK)
                                                                        WHERE UserId = '{userId}' AND PointOfUser.VendorId = c.VendorId AND 
                                                                            ((Type = {PointOfUserType.Deducted.ToInt()} AND
                                                                            ProgramType = {PointOfUserType.GiftExchange.ToInt()}) OR 
                                                                            (Type <> {PointOfUserType.Deducted.ToInt()})))
                                                      ORDER BY Points DESC) r ON r.VendorId = v.Id
                   LEFT JOIN (SELECT TOP 1 p1.VendorId, IIF(SUM(Point) is not null, SUM(Point), 0) as Point,  l1.ExpirationDate
                              FROM PointOfUser p1 WITH (NOLOCK)
                                INNER JOIN LoyaltyProgram l1 WITH (NOLOCK) ON (p1.ProgramType IN ({PointOfUserType.Qr.ToInt()}, {PointOfUserType.Order.ToInt()}) AND p1.SourceId = l1.Id)
                              WHERE UserId = '{userId}' AND p1.VendorId = {vendorId} AND l1.ExpirationDate > GETUTCDATE()
                              GROUP BY p1.VendorId, l1.ExpirationDate
                              HAVING IIF(SUM(Point) is not null, SUM(Point), 0) > 0
                              ORDER BY l1.ExpirationDate) p on (v.Id = p.VendorId)
                        WHERE v.Id = {vendorId}
                        GROUP BY v.Id, r.Id, r.VendorId, Points, r.Name,
                                p.VendorId, p.Point, p.ExpirationDate";
    }
}