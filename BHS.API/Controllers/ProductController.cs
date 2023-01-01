using BHS.API.Application.Queries.Product;
using BHS.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BHS.API.Controllers;

public class ProductController : BaseController
{
    private readonly IProductQuery _productQuery;

    public ProductController(IProductQuery productQuery)
    {
        _productQuery = productQuery;
    }

    /// <summary>
    ///     Lấy chi tiết sản phẩm
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{productId:int}")]
    public async Task<IActionResult> Get(int productId)
    {
        var result = await _productQuery.GetAsync(productId);
        return Ok(result);
    }

    /// <summary>
    ///     Tìm kiếm sản phẩm
    /// </summary>
    /// <param name="productName"></param>
    /// <param name="queryTemplate"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{productName}")]
    public async Task<IActionResult> Get(string productName, [FromQuery] QueryTemplate queryTemplate)
    {
        var result = await _productQuery.GetProductByNameAsync(productName, queryTemplate);
        return Ok(result);
    }

    /// <summary>
    ///     Lấy sản phẩm theo category
    /// </summary>
    /// <param name="categoryId"></param>
    /// <param name="queryTemplate"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("Category/{categoryId:int}")]
    public async Task<IActionResult> GetProductOfCategory(int categoryId, [FromQuery] QueryTemplate queryTemplate)
    {
        return Ok(await _productQuery.GetProductOfCategoryAsync(categoryId, queryTemplate));
    }

    /// <summary>
    ///     Lấy sản phẩm theo vendor
    /// </summary>
    /// <param name="vendorId"></param>
    /// <param name="queryTemplate"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("Vendor/{vendorId:int}")]
    public async Task<IActionResult> GetProductOfVendor(int vendorId, [FromQuery] QueryTemplate queryTemplate)
    {
        return Ok(await _productQuery.GetProductOfVendorAsync(vendorId, queryTemplate));
    }

    /// <summary>
    ///     Lấy sản phẩm khuyến mãi/không khuyến mãi
    /// </summary>
    /// <param name="promoFlag"></param>
    /// <param name="queryTemplate"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("Promotion")]
    public async Task<IActionResult> GetAllPromotionalProduct(bool promoFlag, [FromQuery] QueryTemplate queryTemplate)
    {
        var result = await _productQuery.GetAllProductPromosAsync(promoFlag, queryTemplate);
        return Ok(result);
    }

    /// <summary>
    ///     Lấy sản phẩm đã xem/gợi ý
    /// </summary>
    /// <param name="isViewed"></param>
    /// <param name="queryTemplate"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{isViewed:bool}")]
    public async Task<IActionResult> GetProductForUser(bool isViewed, [FromQuery] QueryTemplate queryTemplate)
    {
        return Ok(await _productQuery.GetProductForUserAsync(isViewed, queryTemplate));
    }
}