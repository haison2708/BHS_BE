using BHS.API.Application.Commands.FortuneCommand;
using BHS.API.Application.Queries.Fortune;
using BHS.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BHS.API.Controllers;

public class FortuneController : BaseController
{
    private readonly IFortuneQuery _fortuneQuery;
    private readonly IMediator _mediator;

    public FortuneController(IMediator mediator, IFortuneQuery fortuneQuery)
    {
        _mediator = mediator;
        _fortuneQuery = fortuneQuery;
    }

    #region Get

    /// <summary>
    ///     Lấy tất cả chương trình vòng quay
    /// </summary>
    /// <param name="vendorId"></param>
    /// <param name="queryTemplate"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(int vendorId, [FromQuery] QueryTemplate queryTemplate)
    {
        return Ok(await _fortuneQuery.GetAllAsync(vendorId, queryTemplate));
    }

    /// <summary>
    ///     Lấy chi tiết chương trình vòng quay
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _fortuneQuery.GetAsync(id));
    }

    #endregion

    #region Post

    /// <summary>
    ///     Khởi tạo chương trình vòng quay
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFortune request)
    {
        return Ok(await _mediator.Send(request));
    }

    /// <summary>
    ///     Khởi tạo quà của chương trình vòng quay
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("FortuneDetail")]
    public async Task<IActionResult> Create([FromBody] CreateFortuneDetail request)
    {
        return Ok(await _mediator.Send(request));
    }

    #endregion

    #region put

    /// <summary>
    ///     Cập nhật ảnh cho chương trình vòng quay
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> AddBanner([FromForm] AddBannerFortune request)
    {
        return Ok(await _mediator.Send(request));
    }

    /// <summary>
    ///     Cập nhật ảnh cho phần quà chương trình vòng quay
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("FortuneDetail")]
    public async Task<IActionResult> AddImage([FromForm] AddFortuneDetailImage request)
    {
        return Ok(await _mediator.Send(request));
    }

    #endregion
}