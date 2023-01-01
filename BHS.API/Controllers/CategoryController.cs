using BHS.API.Application.Queries.Category;
using BHS.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BHS.API.Controllers;

public class CategoryController : BaseController
{
    private readonly ICategoryQuery _categoryQuery;

    public CategoryController(ICategoryQuery categoryQuery)
    {
        _categoryQuery = categoryQuery;
    }

    #region Get

    /// <summary>
    ///     Lấy tất cả category của vendor đang chọn
    /// </summary>
    /// <param name="queryTemplate"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryTemplate queryTemplate)
    {
        return Ok(await _categoryQuery.GetAllAsync(queryTemplate));
    }


    [Route("AAA")]
    [HttpPost]
    public async Task<IActionResult> Get()
    {
        return await BaseActionResult(null);
    }

    #endregion
}