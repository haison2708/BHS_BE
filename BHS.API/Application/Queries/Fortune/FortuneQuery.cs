using BHS.API.Services;
using BHS.API.ViewModels;
using BHS.API.ViewModels.Fortunes;
using BHS.API.ViewModels.Vendor;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BHS.API.Application.Queries.Fortune;

public class FortuneQuery : BaseQuery, IFortuneQuery
{
    public FortuneQuery(IConfiguration configuration, IIdentityService identityService) : base(configuration,
        identityService)
    {
    }

    public async Task<object> GetAllAsync(int vendorId, QueryTemplate queryTemplate)
    {
        queryTemplate.PageIndex = queryTemplate.PageIndex < 1 ? 0 : queryTemplate.PageIndex - 1;
        /*var pagingSql = pageSize < 1 ? "" : $@" OFFSET {pageSize * pageIndex} ROWS FETCH NEXT {pageSize} ROWS ONLY";*/

        var sql = Query.GetAllFortuneByVendorQuery(IdentityService.GetUserIdentity(),
            vendorId == 0 ? IdentityService.GetCurrentVendorId() : vendorId);
        using (var connection = new SqlConnection(ConnectionString))
        {
            var result = new Dictionary<int, FortuneViewModel>();
            await connection
                .QueryAsync<FortuneViewModel, FortuneDetailViewModel, VendorViewModel, FortuneViewModel>(sql,
                    (fortune, fortuneDetail, vendor) =>
                    {
                        if (!result.TryGetValue(fortune.Id, out var fortuneViewModel))
                            result.Add(fortune.Id, fortuneViewModel = fortune);
                        fortuneViewModel.FortuneDetails ??= new List<FortuneDetailViewModel>();
                        if (fortuneDetail is not null)
                            fortuneViewModel.FortuneDetails.Add(fortuneDetail);
                        fortuneViewModel.Vendor = vendor;
                        return fortuneViewModel;
                    }, "Id");
            return new PaginatedItemsViewModel<FortuneViewModel>(queryTemplate.PageIndex, queryTemplate.PageSize,
                result.Values.AsList().Count,
                result.Values.AsList().Skip(queryTemplate.PageSize * queryTemplate.PageIndex)
                    .Take(queryTemplate.PageSize));
        }
    }

    public async Task<object> GetAsync(int id)
    {
        var sql = Query.GetFortuneQuery(id, IdentityService.GetUserIdentity());
        await using var connection = new SqlConnection(ConnectionString);
        var result = new Dictionary<int, FortuneViewModel>();
        await connection
            .QueryAsync<FortuneViewModel, FortuneDetailViewModel, VendorViewModel, FortuneViewModel>(sql,
                (fortune, fortuneDetail, vendor) =>
                {
                    if (!result.TryGetValue(fortune.Id, out var fortuneViewModel))
                        result.Add(fortune.Id, fortuneViewModel = fortune);
                    fortuneViewModel.FortuneDetails ??= new List<FortuneDetailViewModel>();
                    if (fortuneDetail is not null)
                        fortuneViewModel.FortuneDetails.Add(fortuneDetail);
                    fortuneViewModel.Vendor = vendor;
                    return fortuneViewModel;
                }, "Id");
        return result.Values.FirstOrDefault()!;
    }
}