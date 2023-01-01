using BHS.API.Services;
using BHS.API.ViewModels;
using BHS.API.ViewModels.Category;
using BHS.API.ViewModels.Vendor;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BHS.API.Application.Queries.Category;

public class CategoryQuery : BaseQuery, ICategoryQuery
{
    public CategoryQuery(IConfiguration configuration, IIdentityService identityService) : base(configuration,
        identityService)
    {
    }

    public async Task<object> GetAllAsync(QueryTemplate queryTemplate)
    {
        queryTemplate.PageIndex = queryTemplate.PageIndex < 1 ? 0 : queryTemplate.PageIndex - 1;
        var sql = Query.GetAllCategoriesQuery(IdentityService.GetCurrentVendorId());
        await using var connection = new SqlConnection(ConnectionString);
        var result = new Dictionary<int, CategoryViewModel>();
        await connection.QueryAsync<CategoryViewModel, VendorViewModel, CategoryViewModel>(sql, (pc, _) =>
        {
            if (!result.TryGetValue(pc.Id, out var categoryViewModel) && pc.Level == 1)
            {
                result.Add(pc.Id, categoryViewModel = pc);
                categoryViewModel.Category ??= new List<CategoryViewModel>();
            }

            if (pc.Level == 2 && result[pc.ParentId].Category!.All(x => x.Id != pc.Id))
                result[pc.ParentId].Category!.Add(pc);
            return categoryViewModel!;
        });
        return new PaginatedItemsViewModel<CategoryViewModel>(queryTemplate.PageIndex, queryTemplate.PageSize,
            result.Values.AsList().Count,
            result.Values.AsList().Skip(queryTemplate.PageSize * queryTemplate.PageIndex)
                .Take(queryTemplate.PageSize));
    }
}