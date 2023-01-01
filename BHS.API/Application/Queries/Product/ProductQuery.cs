using BHS.API.Application.Commands.ProductCommand;
using BHS.API.Services;
using BHS.API.ViewModels;
using BHS.API.ViewModels.Products;
using BHS.API.ViewModels.Vendor;
using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;

namespace BHS.API.Application.Queries.Product;

public class ProductQuery : BaseQuery, IProductQuery
{
    private readonly IMediator _mediator;

    public ProductQuery(IConfiguration configuration, IMediator mediator, IIdentityService identityService) : base(
        configuration, identityService)
    {
        _mediator = mediator;
    }

    public async Task<object> GetAsync(int productId)
    {
        await _mediator.Send(new CreateProductForUser { ProductId = productId });
        var sql = Query.GetProduct(productId);
        await using var connection = new SqlConnection(ConnectionString);
        var result = new Dictionary<int, ParentProductViewModel>();
        var childResult = new Dictionary<int, ProductViewModel>();
        var attributes = new Dictionary<int, AttributesViewModel>();
        var images = new Dictionary<int, ProductImagesViewModel>();

        await connection
            .QueryAsync<ParentProductViewModel, ProductViewModel, VendorViewModel, ProductImagesViewModel,
                PromotionalProductViewModel,
                AttributesViewModel, AttributeValueViewModel, ParentProductViewModel>(sql,
                (pp, p, v, i, ppm, a, av) =>
                {
                    if (!result.TryGetValue(pp.Id, out var parentProductViewModel))
                        result.Add(pp.Id, parentProductViewModel = pp);

                    if (!childResult.TryGetValue(p.Id, out var productViewModel))
                        childResult.Add(p.Id, productViewModel = p);

                    parentProductViewModel.Vendor = v;

                    if (i != null)
                    {
                        if (!images.TryGetValue(i.Id, out var productImagesViewModel))
                            images.Add(i.Id, productImagesViewModel = i);
                        productViewModel.ProductImages ??= new List<ProductImagesViewModel>();
                        productViewModel.ProductImages.Add(productImagesViewModel);
                    }

                    if (av is not null && a is not null)
                    {
                        if (!attributes.TryGetValue(a.Id, out var attributesViewModel))
                            attributes.Add(a.Id, attributesViewModel = a);
                        productViewModel.AttributeValues ??= new List<AttributeValueViewModel>();
                        productViewModel.AttributeValues.Add(av);
                        attributesViewModel.AttributeValues ??= new List<string>();
                        if (!attributesViewModel.AttributeValues.Contains(av.Value!))
                            attributesViewModel.AttributeValues.Add(av.Value!);
                    }

                    parentProductViewModel.Products ??= new List<ProductViewModel>();
                    if (parentProductViewModel.Products.All(x => x.Id != productViewModel.Id))
                        parentProductViewModel.Products.Add(productViewModel);

                    if (ppm is null) return parentProductViewModel;
                    if (ppm.ProductId == p.Id)
                        p.PricePromotion = ppm.PercentPromo is > 0 and <= 100
                            ? p.Price - p.Price * ppm.PercentPromo / 100
                            : ppm.AmountPromo > 0
                                ? p.Price - ppm.AmountPromo
                                : p.Price;
                    p.IsPromotion = true;
                    p.PromotionTag = "true";

                    return parentProductViewModel;
                }, splitOn: "Id"
            );
        return new
        {
            Product = result.Values.FirstOrDefault()!, Attributes = attributes.Values.FirstOrDefault()!
        };
    }

    public async Task<PaginatedItemsViewModel<ProductViewModel>> GetProductForUserAsync(bool isViewed,
        QueryTemplate queryTemplate)
    {
        var sql = Query.GetProductForUser(IdentityService.GetUserIdentity(), IdentityService.GetCurrentVendorId(),
            isViewed, queryTemplate);
        await using var connection = new SqlConnection(ConnectionString);
        var query = await connection.QueryMultipleAsync(sql);
        var result = query.Read<ProductViewModel, PromotionalProductViewModel, ProductViewModel>(
            (p, pp) =>
            {
                if (pp is null) return p;
                if (pp.ProductId == p.Id)
                    p.PricePromotion = pp.PercentPromo is > 0 and <= 100
                        ? p.Price - p.Price * pp.PercentPromo / 100
                        : pp.AmountPromo > 0
                            ? p.Price - pp.AmountPromo
                            : p.Price;
                p.IsPromotion = true;
                p.PromotionTag = "true";

                return p;
            }).AsList();
        return new PaginatedItemsViewModel<ProductViewModel>(queryTemplate.PageIndex, queryTemplate.PageSize,
            query.Read<long>().FirstOrDefault(), result);
    }

    public async Task<PaginatedItemsViewModel<ProductViewModel>> GetProductOfCategoryAsync(int categoryId,
        QueryTemplate queryTemplate)
    {
        var sql = Query.GetProductOfCategory(IdentityService.GetCurrentVendorId(), categoryId, queryTemplate);
        await using var connection = new SqlConnection(ConnectionString);
        var query = await connection.QueryMultipleAsync(sql);
        var result = query.Read<ProductViewModel, PromotionalProductViewModel, ProductViewModel>(
            (p, pp) =>
            {
                if (pp is null) return p;
                if (pp.ProductId == p.Id)
                    p.PricePromotion = pp.PercentPromo is > 0 and <= 100
                        ? p.Price - p.Price * pp.PercentPromo / 100
                        : pp.AmountPromo > 0
                            ? p.Price - pp.AmountPromo
                            : p.Price;
                p.IsPromotion = true;
                p.PromotionTag = "true";

                return p;
            }).AsList();
        return new PaginatedItemsViewModel<ProductViewModel>(queryTemplate.PageIndex, queryTemplate.PageSize,
            query.Read<long>().FirstOrDefault(), result);
    }

    public async Task<PaginatedItemsViewModel<ProductViewModel>> GetProductOfVendorAsync(int vendorId,
        QueryTemplate queryTemplate)
    {
        var sql = Query.GetProductOfVendor(vendorId, queryTemplate);
        await using var connection = new SqlConnection(ConnectionString);
        var query = await connection.QueryMultipleAsync(sql);
        var result = query.Read<ProductViewModel, PromotionalProductViewModel, ProductViewModel>(
            (p, pp) =>
            {
                if (pp is null) return p;
                if (pp.ProductId == p.Id)
                    p.PricePromotion = pp.PercentPromo is > 0 and <= 100
                        ? p.Price - p.Price * pp.PercentPromo / 100
                        : pp.AmountPromo > 0
                            ? p.Price - pp.AmountPromo
                            : p.Price;
                p.IsPromotion = true;
                p.PromotionTag = "true";

                return p;
            }).AsList();
        return new PaginatedItemsViewModel<ProductViewModel>(queryTemplate.PageIndex, queryTemplate.PageSize,
            query.Read<long>().FirstOrDefault(), result);
    }

    public async Task<PaginatedItemsViewModel<ProductViewModel>> GetProductByNameAsync(string productName,
        QueryTemplate queryTemplate)
    {
        var sql = Query.GetProductByName(IdentityService.GetCurrentVendorId(), productName, queryTemplate);
        await using var connection = new SqlConnection(ConnectionString);
        var query = await connection.QueryMultipleAsync(sql);
        var result = query.Read<ProductViewModel, PromotionalProductViewModel, ProductViewModel>(
            (p, pp) =>
            {
                if (pp is null) return p;
                if (pp.ProductId == p.Id)
                    p.PricePromotion = pp.PercentPromo is > 0 and <= 100
                        ? p.Price - p.Price * pp.PercentPromo / 100
                        : pp.AmountPromo > 0
                            ? p.Price - pp.AmountPromo
                            : p.Price;
                p.IsPromotion = true;
                p.PromotionTag = "true";

                return p;
            }).AsList();
        return new PaginatedItemsViewModel<ProductViewModel>(queryTemplate.PageIndex, queryTemplate.PageSize,
            query.Read<long>().FirstOrDefault(), result);
    }

    public async Task<object> GetAllProductPromosAsync(bool promoFlag, QueryTemplate queryTemplate)
    {
        var sql = Query.GetAllProductPromos(IdentityService.GetCurrentVendorId(), promoFlag, queryTemplate);
        await using var connection = new SqlConnection(ConnectionString);
        var query = await connection.QueryMultipleAsync(sql);
        var result = query.Read<ProductViewModel, PromotionalProductViewModel, ProductViewModel>(
            (p, pp) =>
            {
                if (pp is null) return p;
                if (pp.ProductId == p.Id)
                    p.PricePromotion = pp.PercentPromo is > 0 and <= 100
                        ? p.Price - p.Price * pp.PercentPromo / 100
                        : pp.AmountPromo > 0
                            ? p.Price - pp.AmountPromo
                            : p.Price;
                p.IsPromotion = true;
                p.PromotionTag = "true";

                return p;
            }).ToList();
        return new PaginatedItemsViewModel<ProductViewModel>(queryTemplate.PageIndex, queryTemplate.PageSize,
            query.Read<long>().FirstOrDefault(), result);
    }
}