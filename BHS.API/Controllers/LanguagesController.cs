using BHS.API.Application.Queries.Language;
using Microsoft.AspNetCore.Mvc;

namespace BHS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LanguagesController : BaseController
{
    private readonly ILanguageQuery _languageQuery;

    public LanguagesController(ILanguageQuery languageQuery)
    {
        _languageQuery = languageQuery;
    }

    #region Get

    /// <summary>
    ///     Lấy tất cả ngôn ngữ
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _languageQuery.GetAllLanguageAsync());
    }

    #endregion
}