using BHS.API.ViewModels;
using BHS.API.ViewModels.Products;
using BHS.Domain.SeedWork;

namespace BHS.API.Application.Queries.Product;

public interface IProductQuery : IQuery
{
    Task<object> GetAsync(int id);

    Task<PaginatedItemsViewModel<ProductViewModel>> GetProductOfCategoryAsync(int categoryId,
        QueryTemplate queryTemplate);

    Task<object> GetAllProductPromosAsync(bool promoFlag, QueryTemplate queryTemplate);
    Task<PaginatedItemsViewModel<ProductViewModel>> GetProductForUserAsync(bool isViewed, QueryTemplate queryTemplate);
    Task<PaginatedItemsViewModel<ProductViewModel>> GetProductOfVendorAsync(int vendorId, QueryTemplate queryTemplate);

    Task<PaginatedItemsViewModel<ProductViewModel>> GetProductByNameAsync(string productName,
        QueryTemplate queryTemplate);
}