using BHS.API.ViewModels;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.API.Application.Queries.Product;

public class Query
{
    public static string GetProduct(int productId)
    {
        return $@"SELECT pp.* , p.* , ve.*, i.*, ppm.*, a.*, av.*
                            FROM ParentProduct pp  WITH (NOLOCK)
                            INNER JOIN Product p WITH (NOLOCK) on pp.Id = p.ParentProductId 
                            INNER JOIN Category c WITH (NOLOCK) on pp.CategoryId = c.Id 
                            INNER JOIN CategoryOfVendor v WITH (NOLOCK) on v.CategoryId = c.Id
                            INNER JOIN Vendor ve WITH (NOLOCK) on ve.Id = pp.VendorId
                            LEFT JOIN ProductImage i WITH (NOLOCK) on p.Id = i.ProductId
                            LEFT JOIN PromotionalProduct ppm WITH (NOLOCK) on (p.Id = ppm.ProductId AND ppm.StartAt <= GETUTCDATE() AND ppm.EndAt >= GETUTCDATE() AND ppm.Status = {CommonStatus.Active.ToInt()})
                            LEFT JOIN AttributeValue av WITH (NOLOCK) ON av.ProductId = p.Id
                            LEFT JOIN Attributes a WITH (NOLOCK) ON av.AttributesId = a.Id
                            WHERE pp.Id = (SELECT ParentProductId FROM Product WHERE Id = {productId})";
    }
    
    public static string GetProductForUser(string userId, int vendorId, bool isViewed, QueryTemplate queryTemplate)
    {
        queryTemplate.PageIndex = queryTemplate.PageIndex < 1 ? 0 : queryTemplate.PageIndex - 1;
        var pagingSql = queryTemplate.PageSize < 1
            ? ""
            : $@" OFFSET {queryTemplate.PageSize * queryTemplate.PageIndex} ROWS FETCH NEXT {queryTemplate.PageSize} ROWS ONLY";
        var filter = isViewed
            ? $@"pu.Type = {ProductForUserType.Viewed.ToInt()}"
            : $@"pu.Type = {ProductForUserType.Suggestion.ToInt()}";
        return $@"SELECT DISTINCT p.*, pp.* FROM Product p WITH (NOLOCK) 
                       INNER JOIN ProductForUser pu WITH (NOLOCK) on pu.ProductId = p.Id 
                       INNER JOIN ParentProduct pr WITH (NOLOCK) on pr.Id = p.ParentProductId 
                       INNER JOIN Category c WITH (NOLOCK)  on pr.CategoryId = c.Id
                       INNER JOIN CategoryOfVendor cv WITH (NOLOCK) on c.Id = cv.CategoryId
                       LEFT JOIN PromotionalProduct pp WITH (NOLOCK) on (p.Id = pp.ProductId AND pp.StartAt <= GETUTCDATE() AND pp.EndAt >= GETUTCDATE() AND pp.Status = {CommonStatus.Active.ToInt()})
                       where cv.Status = {CommonStatus.Active.ToInt()}
                       AND p.IsShow = 1 AND p.IsDeleted = 0 AND pu.UserId = '{userId}' AND {filter}
                       AND c.Status = {CommonStatus.Active.ToInt()} AND c.IsDeleted = 0 AND pr.Status = {CommonStatus.Active.ToInt()} AND pr.VendorId = {vendorId} 
                       ORDER BY p.CreatedAt desc 
                        {pagingSql};
                        SELECT COUNT(DISTINCT p.Id) FROM Product p WITH (NOLOCK) 
                       INNER JOIN ProductForUser pu WITH (NOLOCK) on pu.ProductId = p.Id 
                       INNER JOIN ParentProduct pr WITH (NOLOCK) on pr.Id = p.ParentProductId 
                       INNER JOIN Category c WITH (NOLOCK)  on pr.CategoryId = c.Id
                       INNER JOIN CategoryOfVendor cv WITH (NOLOCK) on c.Id = cv.CategoryId
                       LEFT JOIN PromotionalProduct pp WITH (NOLOCK) on (p.Id = pp.ProductId AND pp.StartAt <= GETUTCDATE() AND pp.EndAt >= GETUTCDATE() AND pp.Status = {CommonStatus.Active.ToInt()})
                       where cv.Status = {CommonStatus.Active.ToInt()}
                       AND p.IsShow = 1 AND p.IsDeleted = 0 AND pu.UserId = '{userId}' AND {filter}
                       AND c.Status = {CommonStatus.Active.ToInt()} AND c.IsDeleted = 0 AND pr.Status = {CommonStatus.Active.ToInt()} AND pr.VendorId = {vendorId} ";
    }

    public static string GetProductOfCategory(int vendorId, int categoryId, QueryTemplate queryTemplate)
    {
        queryTemplate.PageIndex = queryTemplate.PageIndex < 1 ? 0 : queryTemplate.PageIndex - 1;
        var pagingSql = queryTemplate.PageSize < 1
            ? ""
            : $@" OFFSET {queryTemplate.PageSize * queryTemplate.PageIndex} ROWS FETCH NEXT {queryTemplate.PageSize} ROWS ONLY";

        return $@"SELECT DISTINCT p.*, pp.* FROM Product p WITH (NOLOCK) 
                       INNER JOIN ParentProduct pr WITH (NOLOCK) on pr.Id = p.ParentProductId 
                       INNER JOIN Category c WITH (NOLOCK)  on pr.CategoryId = c.Id
                       INNER JOIN CategoryOfVendor cv WITH (NOLOCK)  on cv.CategoryId = c.Id
                       LEFT JOIN PromotionalProduct pp WITH (NOLOCK) on  (p.Id = pp.ProductId AND pp.StartAt <= GETUTCDATE() AND pp.EndAt >= GETUTCDATE() AND pp.Status = {CommonStatus.Active.ToInt()})
                       where c.Id = {categoryId} AND p.IsShow = 1 AND p.IsDeleted = 0 
                       AND c.Status = {CommonStatus.Active.ToInt()} AND c.IsDeleted = 0 AND pr.Status = {CommonStatus.Active.ToInt()}
                       AND cv.VendorId = {vendorId} AND cv.Status = {CommonStatus.Active.ToInt()} 
                       AND pr.VendorId = {vendorId} 
                       ORDER BY p.CreatedAt desc
                        {pagingSql}; 
                        SELECT COUNT(DISTINCT p.Id) FROM Product p WITH (NOLOCK) 
                       INNER JOIN ParentProduct pr WITH (NOLOCK) on pr.Id = p.ParentProductId 
                       INNER JOIN Category c WITH (NOLOCK)  on pr.CategoryId = c.Id
                       INNER JOIN CategoryOfVendor cv WITH (NOLOCK)  on cv.CategoryId = c.Id
                       LEFT JOIN PromotionalProduct pp WITH (NOLOCK) on  (p.Id = pp.ProductId AND pp.StartAt <= GETUTCDATE() AND pp.EndAt >= GETUTCDATE() AND pp.Status = {CommonStatus.Active.ToInt()})
                       where c.Id = {categoryId} AND p.IsShow = 1 AND p.IsDeleted = 0 
                       AND c.Status = {CommonStatus.Active.ToInt()} AND c.IsDeleted = 0 AND pr.Status = {CommonStatus.Active.ToInt()}
                       AND cv.VendorId = {vendorId} AND cv.Status = {CommonStatus.Active.ToInt()}
                       AND pr.VendorId = {vendorId} ";
    }

    public static string GetProductOfVendor(int vendorId, QueryTemplate queryTemplate)
    {
        queryTemplate.PageIndex = queryTemplate.PageIndex < 1 ? 0 : queryTemplate.PageIndex - 1;
        var pagingSql = queryTemplate.PageSize < 1
            ? ""
            : $@" OFFSET {queryTemplate.PageSize * queryTemplate.PageIndex} ROWS FETCH NEXT {queryTemplate.PageSize} ROWS ONLY";

        return $@"SELECT DISTINCT p.*, pp.* FROM Product p WITH (NOLOCK) 
                       INNER JOIN ParentProduct pr WITH (NOLOCK) on pr.Id = p.ParentProductId 
                       INNER JOIN Category c WITH (NOLOCK)  on pr.CategoryId = c.Id
                       INNER JOIN CategoryOfVendor cv WITH (NOLOCK)  on cv.CategoryId = c.Id
                       LEFT JOIN PromotionalProduct pp WITH (NOLOCK) on  (p.Id = pp.ProductId AND pp.StartAt <= GETUTCDATE() AND pp.EndAt >= GETUTCDATE() AND pp.Status = {CommonStatus.Active.ToInt()})
                       where p.IsShow = 1 AND p.IsDeleted = 0 
                       AND c.Status = {CommonStatus.Active.ToInt()} AND c.IsDeleted = 0 AND pr.Status = {CommonStatus.Active.ToInt()}
                       AND cv.VendorId = {vendorId} AND cv.Status = {CommonStatus.Active.ToInt()} AND pr.VendorId = {vendorId} 
                       ORDER BY p.CreatedAt desc
                        {pagingSql}; 
                        SELECT COUNT(DISTINCT p.Id) FROM Product p WITH (NOLOCK) 
                       INNER JOIN ParentProduct pr WITH (NOLOCK) on pr.Id = p.ParentProductId 
                       INNER JOIN Category c WITH (NOLOCK)  on pr.CategoryId = c.Id
                       INNER JOIN CategoryOfVendor cv WITH (NOLOCK)  on cv.CategoryId = c.Id
                       LEFT JOIN PromotionalProduct pp WITH (NOLOCK) on  (p.Id = pp.ProductId AND pp.StartAt <= GETUTCDATE() AND pp.EndAt >= GETUTCDATE() AND pp.Status = {CommonStatus.Active.ToInt()})
                       where p.IsShow = 1 AND p.IsDeleted = 0 
                       AND c.Status = {CommonStatus.Active.ToInt()} AND c.IsDeleted = 0 AND pr.Status = {CommonStatus.Active.ToInt()}
                       AND cv.VendorId = {vendorId} AND cv.Status = {CommonStatus.Active.ToInt()} AND pr.VendorId = {vendorId} ";
    }

    public static string GetProductByName(int vendorId, string productName, QueryTemplate queryTemplate)
    {
        queryTemplate.PageIndex = queryTemplate.PageIndex < 1 ? 0 : queryTemplate.PageIndex - 1;
        var pagingSql = queryTemplate.PageSize < 1
            ? ""
            : $@" OFFSET {queryTemplate.PageSize * queryTemplate.PageIndex} ROWS FETCH NEXT {queryTemplate.PageSize} ROWS ONLY";

        return $@"SELECT DISTINCT p.*, pp.* FROM Product p WITH (NOLOCK) 
                       INNER JOIN ParentProduct pr WITH (NOLOCK) on pr.Id = p.ParentProductId 
                       INNER JOIN Category c WITH (NOLOCK)  on pr.CategoryId = c.Id
                       INNER JOIN CategoryOfVendor cv WITH (NOLOCK)  on cv.CategoryId = c.Id
                       LEFT JOIN PromotionalProduct pp WITH (NOLOCK) on  (p.Id = pp.ProductId AND pp.StartAt <= GETUTCDATE() AND pp.EndAt >= GETUTCDATE() AND pp.Status = {CommonStatus.Active.ToInt()})
                       where p.IsShow = 1 AND p.IsDeleted = 0 
                       AND c.Status = {CommonStatus.Active.ToInt()} AND c.IsDeleted = 0 AND pr.Status = {CommonStatus.Active.ToInt()}
                       AND cv.VendorId = {vendorId} AND cv.Status = {CommonStatus.Active.ToInt()} 
                       AND pr.VendorId = {vendorId} 
                       ORDER BY p.CreatedAt desc
                        {pagingSql}; 
                        SELECT COUNT(DISTINCT p.Id) FROM Product p WITH (NOLOCK) 
                       INNER JOIN ParentProduct pr WITH (NOLOCK) on pr.Id = p.ParentProductId 
                       INNER JOIN Category c WITH (NOLOCK)  on pr.CategoryId = c.Id
                       INNER JOIN CategoryOfVendor cv WITH (NOLOCK)  on cv.CategoryId = c.Id
                       LEFT JOIN PromotionalProduct pp WITH (NOLOCK) on  (p.Id = pp.ProductId AND pp.StartAt <= GETUTCDATE() AND pp.EndAt >= GETUTCDATE() AND pp.Status = {CommonStatus.Active.ToInt()})
                       where p.IsShow = 1 AND p.IsDeleted = 0 AND p.Name LIKE N'%{productName}%'
                       AND c.Status = {CommonStatus.Active.ToInt()} AND c.IsDeleted = 0 AND pr.Status = {CommonStatus.Active.ToInt()}
                       AND cv.VendorId = {vendorId} AND cv.Status = {CommonStatus.Active.ToInt()}
                       AND pr.VendorId = {vendorId} ";
    }

    public static string GetAllProductPromos(int vendorId, bool promoFlag, QueryTemplate queryTemplate)
    {
        queryTemplate.PageIndex = queryTemplate.PageIndex < 1 ? 0 : queryTemplate.PageIndex - 1;
        var pagingSql = queryTemplate.PageSize < 1
            ? ""
            : $@" OFFSET {queryTemplate.PageSize * queryTemplate.PageIndex} ROWS FETCH NEXT {queryTemplate.PageSize} ROWS ONLY";
        var filter = promoFlag ? @"INNER " : @"LEFT ";

        return $@"SELECT DISTINCT p.*, pp.* FROM Product p WITH (NOLOCK) 
                       INNER JOIN ParentProduct pr WITH (NOLOCK) on pr.Id = p.ParentProductId 
                       INNER JOIN Category c WITH (NOLOCK)  on pr.CategoryId = c.Id
                       INNER JOIN CategoryOfVendor cv WITH (NOLOCK) on c.Id = cv.CategoryId
                       {filter} JOIN PromotionalProduct pp WITH (NOLOCK) on  (p.Id = pp.ProductId AND pp.StartAt <= GETUTCDATE() AND pp.EndAt >= GETUTCDATE() 
                AND pp.Status = {CommonStatus.Active.ToInt()})
                       where cv.VendorId = {vendorId} AND cv.Status = {CommonStatus.Active.ToInt()}
                       AND p.IsShow = 1 AND p.IsDeleted = 0
                       AND c.Status = {CommonStatus.Active.ToInt()} AND c.IsDeleted = 0 AND pr.Status = {CommonStatus.Active.ToInt()}
                       AND pr.VendorId = {vendorId} 
                       ORDER BY p.CreatedAt desc
                       {pagingSql};
                       SELECT COUNT(DISTINCT p.Id) FROM Product p WITH (NOLOCK) 
                       INNER JOIN ParentProduct pr WITH (NOLOCK) on pr.Id = p.ParentProductId 
                       INNER JOIN Category c WITH (NOLOCK)  on pr.CategoryId = c.Id
                       INNER JOIN CategoryOfVendor cv WITH (NOLOCK) on c.Id = cv.CategoryId
                       {filter} JOIN PromotionalProduct pp WITH (NOLOCK) on  (p.Id = pp.ProductId AND pp.StartAt <= GETUTCDATE() AND pp.EndAt >= GETUTCDATE() 
                AND pp.Status = {CommonStatus.Active.ToInt()})
                       where cv.VendorId = {vendorId} AND cv.Status = {CommonStatus.Active.ToInt()}
                       AND p.IsShow = 1 AND p.IsDeleted = 0
                       AND c.Status = {CommonStatus.Active.ToInt()} AND c.IsDeleted = 0 AND pr.Status = {CommonStatus.Active.ToInt()}
                       AND pr.VendorId = {vendorId}";
    }
}