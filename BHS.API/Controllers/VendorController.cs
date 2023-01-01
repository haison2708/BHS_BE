using BHS.API.Application.Commands.VendorCommand;
using BHS.API.Application.Queries.Vendor;
using BHS.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BHS.API.Controllers;

public class VendorController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IVendorQuery _vendorQuery;

    public VendorController(IVendorQuery vendorQuery, IMediator mediator)
    {
        _vendorQuery = vendorQuery;
        _mediator = mediator;
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("ConfigRankOfVendor")]
    public async Task<IActionResult> ConfigRankOfVendor([FromBody] CreateConfigRankOfVendor request)
    {
        return Ok(await _mediator.Send(request));
    }

    #region Get

    /// <summary>
    ///     Lấy tất cả vendor
    /// </summary>
    /// <param name="queryTemplate"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryTemplate queryTemplate)
    {
        return Ok(await _vendorQuery.GetAllAsync(queryTemplate));
    }

    /// <summary>
    ///     Lấy chi tiết vendor
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _vendorQuery.GetAsync(id));
    }

    /// <summary>
    ///     Lấy vendor được user theo dõi
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("User")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _vendorQuery.GetVendorsThatUserFollowsAsync());
    }

    /// <summary>
    ///     Tìm kiếm vendor
    /// </summary>
    /// <param name="vendorName"></param>
    /// <param name="byUser"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{vendorName}")]
    public async Task<IActionResult> GetVendorByName(string vendorName, bool byUser)
    {
        return Ok(await _vendorQuery.GetVendorByNameAsync(vendorName, byUser));
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("ConfigRankOfVendor")]
    public async Task<IActionResult> GetConfigRankOfVendor()
    {
        return Ok(await _vendorQuery.GetConfigRankOfVendorAsync());
    }

    /// <summary>
    ///     Lấy tổng số luượt quay
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("LuckyWheelTurns")]
    public async Task<IActionResult> GetLuckyWheelTurns()
    {
        return Ok(await _vendorQuery.GetLuckyWheelTurns());
    }

    #endregion
}