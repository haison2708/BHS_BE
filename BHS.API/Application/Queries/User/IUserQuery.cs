using BHS.API.ViewModels;
using BHS.API.ViewModels.Cart;
using BHS.API.ViewModels.Users;
using BHS.API.ViewModels.Vendor;
using BHS.Domain.SeedWork;

namespace BHS.API.Application.Queries.User;

public interface IUserQuery : IQuery
{
    Task<PaginatedItemsViewModel<HistoryPointsViewModel>> GetHistoriesPointsAsync(QueryTemplate queryTemplate);

    Task<PaginatedItemsViewModel<TotalPointOfProgramViewModel>>
        GetTotalPointsOfProgramsAsync(QueryTemplate queryTemplate);

    Task<UserSettingsViewModel> GetUserSettingsAsync();
    Task<IList<CartViewModel>> GetCartsAsync();
    Task<PaginatedItemsViewModel<GiftOfUserViewModel>> GetGiftsByTypeAsync(int type, QueryTemplate queryTemplate);
    Task<GiftOfUserViewModel> GetGiftAsync(int giftId);

    Task<PaginatedItemsViewModel<FortuneUserRewardViewModel>> GetHistoriesFortuneAsync(int fortuneId,
        QueryTemplate queryTemplate);

    Task<object> GetTotalPointsAndGiftsAsync();
    Task<IList<VendorViewModel>> VendorOverview();
}