using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;

namespace BHS.API.Application.Queries.Category;

public static class Query
{
    public static string GetAllCategoriesQuery(int vendorId)
    {
        return
            $@"SELECT DISTINCT c.*, v.* FROM Category c WITH (NOLOCK) INNER JOIN CategoryOfVendor cv WITH (NOLOCK) on c.Id = cv.CategoryId
                     INNER JOIN Vendor v WITH (NOLOCK) on v.Id = cv.VendorId
                     WHERE c.Status = {CommonStatus.Active.ToInt()} AND cv.Status = {CommonStatus.Active.ToInt()} AND cv.VendorId = {vendorId}
                     ORDER BY c.Level, c.Name";
    }
}