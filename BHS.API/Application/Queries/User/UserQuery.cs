using BHS.API.Application.Commands.UserCommand;
using BHS.API.Services;
using BHS.API.ViewModels;
using BHS.API.ViewModels.Cart;
using BHS.API.ViewModels.Fortunes;
using BHS.API.ViewModels.LoyaltyPrograms;
using BHS.API.ViewModels.Products;
using BHS.API.ViewModels.Users;
using BHS.API.ViewModels.Vendor;
using BHS.Domain.Entities.Vendors;
using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;

namespace BHS.API.Application.Queries.User;

public class UserQuery : BaseQuery, IUserQuery
{
    private readonly IMediator _mediator;

    public UserQuery(IConfiguration configuration, IMediator mediator, IIdentityService identityService) : base(
        configuration, identityService)
    {
        _mediator = mediator;
    }

    public async Task<PaginatedItemsViewModel<HistoryPointsViewModel>> GetHistoriesPointsAsync(
        QueryTemplate queryTemplate)
    {
        var sql = Query.GetHistoriesPoints(IdentityService.GetUserIdentity(), IdentityService.GetCurrentVendorId(),
            queryTemplate);
        await using var connection = new SqlConnection(ConnectionString);
        var query = await connection.QueryMultipleAsync(sql);
        var result = query.Read<HistoryPointsViewModel>();
        return new PaginatedItemsViewModel<HistoryPointsViewModel>(queryTemplate.PageIndex, queryTemplate.PageSize,
            query.ReadFirstOrDefault<long>(),
            result.AsList());
    }

    public async Task<PaginatedItemsViewModel<TotalPointOfProgramViewModel>> GetTotalPointsOfProgramsAsync(
        QueryTemplate queryTemplate)
    {
        var sql = Query.GetTotalPointOfPrograms(IdentityService.GetUserIdentity(), IdentityService.GetCurrentVendorId(),
            queryTemplate);
        await using var connection = new SqlConnection(ConnectionString);
        var result = (await connection.QueryAsync<TotalPointOfProgramViewModel>(sql)).AsList();
        return new PaginatedItemsViewModel<TotalPointOfProgramViewModel>(queryTemplate.PageIndex,
            queryTemplate.PageSize, result.Count,
            result.Skip(queryTemplate.PageSize * queryTemplate.PageIndex).Take(queryTemplate.PageSize));
    }

    public async Task<IList<CartViewModel>> GetCartsAsync()
    {
        await using var connection = new SqlConnection(ConnectionString);
        var sql = Query.GetCarts(IdentityService.GetUserIdentity());
        var result =
            await connection.QueryAsync<CartViewModel, ParentProductViewModel, VendorViewModel, CartViewModel>(
                sql,
                (cart, product, vendor) =>
                {
                    cart.Vendor = vendor;
                    cart.ParentProduct = product;
                    return cart;
                },
                splitOn: "Id");
        return result.AsList();
    }

    public async Task<PaginatedItemsViewModel<GiftOfUserViewModel>> GetGiftsByTypeAsync(int type,
        QueryTemplate queryTemplate)
    {
        await using var connection = new SqlConnection(ConnectionString);
        var sql = Query.GetGifts(IdentityService.GetUserIdentity(), IdentityService.GetCurrentVendorId(), type,
            queryTemplate);
        var query = await connection.QueryMultipleAsync(sql);

        var result = query.Read<GiftOfUserViewModel, GiftOfLoyaltyViewModel, FortuneViewModel, ProductViewModel,
            GiftOfUserViewModel>((gu, gl, f, p) =>
        {
            gl.Fortune = f;
            gl.Product = p;
            gu.GiftOfLoyalty = gl;
            return gu;
        }).AsList();
        return new PaginatedItemsViewModel<GiftOfUserViewModel>(queryTemplate.PageIndex, queryTemplate.PageSize,
            query.ReadFirstOrDefault<long>(), result);
    }

    public async Task<GiftOfUserViewModel> GetGiftAsync(int giftId)
    {
        await using var connection = new SqlConnection(ConnectionString);
        var sql = Query.GetGift(IdentityService.GetUserIdentity(), IdentityService.GetCurrentVendorId(), giftId);

        var result = await connection
            .QueryAsync<GiftOfUserViewModel, GiftOfLoyaltyViewModel, FortuneViewModel, ProductViewModel,
                LoyaltyProgramViewModel, GiftOfUserViewModel>(sql, (gu, gl, f, p, l) =>
            {
                gl.Fortune = f;
                gl.Product = p;
                gl.LoyaltyProgram = l;
                gu.GiftOfLoyalty = gl;
                return gu;
            });

        return result.FirstOrDefault()!;
    }

    public async Task<PaginatedItemsViewModel<FortuneUserRewardViewModel>> GetHistoriesFortuneAsync(int fortuneId,
        QueryTemplate queryTemplate)
    {
        var sql = Query.GetHistoriesFortune(IdentityService.GetUserIdentity(), fortuneId, queryTemplate);
        await using var connection = new SqlConnection(ConnectionString);
        var query = await connection
            .QueryMultipleAsync(sql);
        var result = query.Read<FortuneViewModel, FortuneUserRewardViewModel, FortuneDetailViewModel,
            FortuneUserRewardViewModel>(
            (fortune, fortuneUserReward, fortuneDetail) =>
            {
                fortune.FortuneDetails ??= new List<FortuneDetailViewModel>();
                fortune.FortuneDetails.Add(fortuneDetail);
                fortuneUserReward.Fortune = fortune;
                return fortuneUserReward;
            },
            "Id").AsList();

        return new PaginatedItemsViewModel<FortuneUserRewardViewModel>(queryTemplate.PageIndex,
            queryTemplate.PageSize,
            query.ReadFirstOrDefault<long>(), result);
    }

    public async Task<object> GetTotalPointsAndGiftsAsync()
    {
        var sql = Query.GetTotalPointsAndGifts(IdentityService.GetUserIdentity(), IdentityService.GetCurrentVendorId());
        await using var connection = new SqlConnection(ConnectionString);
        var result = await connection.QueryAsync<int, int, int, ConfigRankOfVendor, PointOfUserViewModel, object>(
            sql,
            (tg, tp, t, c, p) =>
            {
                RankOfUserViewModel? rankOfUser = null;
                if (c is not null)
                {
                    rankOfUser = new RankOfUserViewModel
                    {
                        Points = tp,
                        Name = c.Name
                    };
                }
                return new { totalGift = tg, totalPoint = tp, rankOfUser, aboutToExpire = p.Point == 0 ? null : p, luckyWheelTurns = t };
            }, splitOn: "TotalGift, TotalPoint, LuckyWheelTurns, Id, VendorId");
        return result.FirstOrDefault()!;
    }

    public async Task<IList<VendorViewModel>> VendorOverview()
    {
        var sql = Query.GetVendorOverview(IdentityService.GetUserIdentity());
        await using var connection = new SqlConnection(ConnectionString);
        var result = new Dictionary<int, VendorViewModel>();
        var configRankDic = new Dictionary<int, ConfigRankOfVendorViewModel>();
        var loyaltyProgramDic = new Dictionary<int, LoyaltyProgramViewModel>();
        var totalPoints = 0;
        await connection
            .QueryAsync<VendorViewModel, ConfigRankOfVendorViewModel, LoyaltyProgramViewModel, PointOfUserViewModel,
                VendorViewModel>(sql,
                (v, r, l, p) =>
                {
                    if (!result.TryGetValue(v.Id, out var vendor))
                        result.Add(v.Id, vendor = v);
                    if (!configRankDic.TryGetValue(r.Id, out var configRank))
                    {
                        configRankDic.Add(r.Id, configRank = r);
                        vendor.ConfigRankOfVendor ??= new List<ConfigRankOfVendorViewModel>();
                        vendor.ConfigRankOfVendor.Add(configRank);
                    }

                    if (!loyaltyProgramDic.TryGetValue(l.Id, out var loyaltyProgram))
                    {
                        loyaltyProgramDic.Add(l.Id, loyaltyProgram = l);
                        vendor.LoyaltyProgram ??= new List<LoyaltyProgramViewModel>();
                        vendor.LoyaltyProgram.Add(loyaltyProgram);
                    }
                    if (vendor.TotalPoint >= r.Points && vendor.TotalPoint >= totalPoints)
                    {
                        totalPoints = vendor.TotalPoint;
                        vendor.RankOfUser = new RankOfUserViewModel
                        {
                            Points = totalPoints,
                            Name = r.Name
                        };
                    }
                    vendor.AboutToExpire = p;
                    
                    return vendor;
                }, splitOn: "Id, VendorId");
        return result.Values.OrderBy(x => x.Id).ToList();
    }

    public async Task<UserSettingsViewModel> GetUserSettingsAsync()
    {
        var sql = $@"SELECT * FROM UserSettings WHERE UserId = '{IdentityService.GetUserIdentity()}'";
        await using var connection = new SqlConnection(ConnectionString);
        var result = await connection.QueryFirstOrDefaultAsync<UserSettingsViewModel>(sql);
        if (result is not null) return result;
        var langId =
            await connection.QueryFirstOrDefaultAsync<string>(
                @"SELECT * FROM Language WHERE Id = 'vi'");
        return await _mediator.Send(new CreateUserSettings
        {
            IsFingerprintLogin = false,
            IsGetNotifications = true,
            LangId = langId
        });
    }
}