using BHS.API.ViewModels;
using BHS.API.ViewModels.Vendor;
using BHS.Domain.SeedWork;

namespace BHS.API.Application.Queries.Vendor;

public interface IVendorQuery : IQuery
{
    Task<object> GetAllAsync(QueryTemplate queryTemplate);
    Task<object> GetAsync(int id);
    Task<IList<VendorViewModel>> GetVendorByNameAsync(string vendorName, bool byUser);
    Task<IList<VendorViewModel>> GetVendorsThatUserFollowsAsync();
    Task<object> GetConfigRankOfVendorAsync();
    Task<object> GetLuckyWheelTurns();
}