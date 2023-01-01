using BHS.API.ViewModels;
using BHS.API.ViewModels.LoyaltyPrograms;
using BHS.API.ViewModels.Vendor;
using BHS.Domain.SeedWork;

namespace BHS.API.Application.Queries.LoyaltyProgram;

public interface ILoyaltyProgramQuery : IQuery
{
    Task<object> GetAllAsync(QueryTemplate queryTemplate);
    Task<object> GetAsync(int id);
    Task<PaginatedItemsViewModel<LoyaltyProgramViewModel>> GetAllGiftAsync(QueryTemplate queryTemplate);
    Task<PaginatedItemsViewModel<VendorViewModel>> GetLoyaltyByNameAsync(string name, QueryTemplate queryTemplate);
}