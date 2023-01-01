using BHS.API.Services;
using BHS.API.ViewModels;
using BHS.API.ViewModels.Fortunes;
using BHS.API.ViewModels.LoyaltyPrograms;
using BHS.API.ViewModels.Products;
using BHS.API.ViewModels.Vendor;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BHS.API.Application.Queries.LoyaltyProgram;

public class LoyaltyProgramQuery : BaseQuery, ILoyaltyProgramQuery
{
    public LoyaltyProgramQuery(IConfiguration configuration, IIdentityService identityService) : base(configuration,
        identityService)
    {
    }

    public async Task<object> GetAsync(int id)
    {
        var sql = Query.GetLoyaltyProgram(IdentityService.GetUserIdentity(), id);
        await using var connection = new SqlConnection(ConnectionString);
        var result = new Dictionary<int, LoyaltyProgramViewModel>();
        var childResult = new Dictionary<int, LoyaltyProgramImageViewModel>();
        var productsParticipatingLoyalty = new Dictionary<int, ProductParticipatingLoyaltyViewModel>();
        var giftsOfLoyalty = new Dictionary<int, GiftOfLoyaltyViewModel>();
        await connection
            .QueryAsync<LoyaltyProgramViewModel, VendorViewModel, LoyaltyProgramImageViewModel,
                ProductParticipatingLoyaltyViewModel, ProductViewModel, GiftOfLoyaltyViewModel, FortuneViewModel
                , LoyaltyProgramViewModel>(sql,
                (l, v, i, p,
                    pr, g, f) =>
                {
                    if (!result.TryGetValue(l.Id, out var loyaltyProgramViewModel))
                        result.Add(l.Id, loyaltyProgramViewModel = l);
                    if (i != null)
                        if (!childResult.TryGetValue(i.Id, out var loyaltyProgramImageViewModel))
                        {
                            childResult.Add(i.Id, loyaltyProgramImageViewModel = i);
                            loyaltyProgramViewModel.LoyaltyProgramImages ??=
                                new List<LoyaltyProgramImageViewModel>();
                            loyaltyProgramViewModel.LoyaltyProgramImages.Add(loyaltyProgramImageViewModel);
                        }

                    if (p != null)
                        if (!productsParticipatingLoyalty.TryGetValue(p.Id,
                                out var productParticipatingLoyaltyViewModel))
                        {
                            productsParticipatingLoyalty.Add(p.Id, productParticipatingLoyaltyViewModel = p);

                            productParticipatingLoyaltyViewModel.Product = pr;
                            loyaltyProgramViewModel.ProductParticipatingLoyalties ??=
                                new List<ProductParticipatingLoyaltyViewModel>();
                            loyaltyProgramViewModel.ProductParticipatingLoyalties.Add(
                                productParticipatingLoyaltyViewModel);
                        }

                    if (g != null)
                        if (!giftsOfLoyalty.TryGetValue(g.Id, out var giftsOfLoyaltyViewModel))
                        {
                            giftsOfLoyalty.Add(g.Id, giftsOfLoyaltyViewModel = g);

                            giftsOfLoyaltyViewModel.Fortune = f;
                            loyaltyProgramViewModel.GiftOfLoyalty ??=
                                new List<GiftOfLoyaltyViewModel>();
                            loyaltyProgramViewModel.GiftOfLoyalty.Add(giftsOfLoyaltyViewModel);
                        }

                    loyaltyProgramViewModel.Vendor = v;
                    return loyaltyProgramViewModel;
                }, splitOn: "Id");
        return result.Values.FirstOrDefault()!;
    }

    public async Task<PaginatedItemsViewModel<LoyaltyProgramViewModel>> GetAllGiftAsync(QueryTemplate queryTemplate)
    {
        var sql = Query.GetAllGifts(IdentityService.GetCurrentVendorId(), queryTemplate);
        await using var connection = new SqlConnection(ConnectionString);
        var result = new Dictionary<int, LoyaltyProgramViewModel>();
        var childResult = new Dictionary<int, LoyaltyProgramImageViewModel>();
        var productsParticipatingLoyalty = new Dictionary<int, ProductParticipatingLoyaltyViewModel>();
        var giftsOfLoyalty = new Dictionary<int, GiftOfLoyaltyViewModel>();
        await connection
            .QueryAsync<LoyaltyProgramViewModel, LoyaltyProgramImageViewModel,
                ProductParticipatingLoyaltyViewModel, ProductViewModel, GiftOfLoyaltyViewModel, FortuneViewModel
                , LoyaltyProgramViewModel>(sql,
                (l, i, p,
                    pr, g, f) =>
                {
                    if (!result.TryGetValue(l.Id, out var loyaltyProgramViewModel))
                        result.Add(l.Id, loyaltyProgramViewModel = l);
                    if (i != null)
                    {
                        if (!childResult.TryGetValue(i.Id, out var loyaltyProgramImageViewModel))
                            childResult.Add(i.Id, loyaltyProgramImageViewModel = i);
                        loyaltyProgramViewModel.LoyaltyProgramImages ??=
                            new List<LoyaltyProgramImageViewModel>();
                        loyaltyProgramViewModel.LoyaltyProgramImages.Add(loyaltyProgramImageViewModel);
                    }

                    if (p != null)
                    {
                        if (!productsParticipatingLoyalty.TryGetValue(p.Id,
                                out var productParticipatingLoyaltyViewModel))
                            productsParticipatingLoyalty.Add(p.Id, productParticipatingLoyaltyViewModel = p);
                        productParticipatingLoyaltyViewModel.Product = pr;
                        loyaltyProgramViewModel.ProductParticipatingLoyalties ??=
                            new List<ProductParticipatingLoyaltyViewModel>();
                        loyaltyProgramViewModel.ProductParticipatingLoyalties.Add(
                            productParticipatingLoyaltyViewModel);
                    }

                    if (g != null)
                    {
                        if (!giftsOfLoyalty.TryGetValue(g.Id, out var giftsOfLoyaltyViewModel))
                            giftsOfLoyalty.Add(g.Id, giftsOfLoyaltyViewModel = g);
                        giftsOfLoyaltyViewModel.Fortune = f;
                        loyaltyProgramViewModel.GiftOfLoyalty ??=
                            new List<GiftOfLoyaltyViewModel>();
                        loyaltyProgramViewModel.GiftOfLoyalty.Add(giftsOfLoyaltyViewModel);
                    }

                    return loyaltyProgramViewModel;
                }, splitOn: "Id");
        return new PaginatedItemsViewModel<LoyaltyProgramViewModel>(queryTemplate.PageIndex, queryTemplate.PageSize,
            result.Values.AsList().Count,
            result.Values.AsList().Skip(queryTemplate.PageSize * queryTemplate.PageIndex)
                .Take(queryTemplate.PageSize));
    }

    public async Task<PaginatedItemsViewModel<VendorViewModel>> GetLoyaltyByNameAsync(string name,
        QueryTemplate queryTemplate)
    {
        var sql = Query.GetLoyaltyProgramsByName(IdentityService.GetUserIdentity(), name, queryTemplate);
        await using var connection = new SqlConnection(ConnectionString);
        var result = new Dictionary<int, VendorViewModel>();
        await connection
            .QueryAsync<LoyaltyProgramViewModel, VendorViewModel, VendorViewModel>(sql, (l, v) =>
            {
                if (!result.TryGetValue(v.Id, out var vendorViewModel))
                    result.Add(v.Id, vendorViewModel = v);
                vendorViewModel.LoyaltyProgram ??= new List<LoyaltyProgramViewModel>();
                vendorViewModel.LoyaltyProgram.Add(l);
                return vendorViewModel;
            });
        return new PaginatedItemsViewModel<VendorViewModel>(queryTemplate.PageIndex, queryTemplate.PageSize,
            result.Values.AsList().Count,
            result.Values.AsList().Skip(queryTemplate.PageSize * queryTemplate.PageIndex)
                .Take(queryTemplate.PageSize));
    }

    public async Task<object> GetAllAsync(QueryTemplate queryTemplate)
    {
        var sql = Query.GetAllLoyaltyProgram(IdentityService.GetUserIdentity(), queryTemplate);
        await using var connection = new SqlConnection(ConnectionString);
        var result = new Dictionary<int, VendorViewModel>();
        await connection
            .QueryAsync<LoyaltyProgramViewModel, VendorViewModel, VendorViewModel>(sql, (l, v) =>
            {
                if (!result.TryGetValue(v.Id, out var vendorViewModel))
                    result.Add(v.Id, vendorViewModel = v);
                vendorViewModel.LoyaltyProgram ??= new List<LoyaltyProgramViewModel>();
                vendorViewModel.LoyaltyProgram.Add(l);
                return vendorViewModel;
            });
        return new PaginatedItemsViewModel<VendorViewModel>(queryTemplate.PageIndex, queryTemplate.PageSize,
            result.Values.AsList().Count,
            result.Values.AsList().Skip(queryTemplate.PageSize * queryTemplate.PageIndex)
                .Take(queryTemplate.PageSize));
    }
}