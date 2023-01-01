using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BHS.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class BaseController : ControllerBase
{
    [NonAction]
    protected Task<IActionResult> BaseActionResult(object? value)
    {
        return value is not null
            ? Task.FromResult<IActionResult>(Ok(value))
            : Task.FromResult<IActionResult>(BadRequest());
    }

    [NonAction]
    protected Task<IActionResult> BaseActionResult(bool value)
    {
        return value ? Task.FromResult<IActionResult>(Ok(value)) : Task.FromResult<IActionResult>(BadRequest());
    }
}