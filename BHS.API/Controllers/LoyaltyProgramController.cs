using BHS.API.Application.Commands.LoyaltyProgramCommand;
using BHS.API.Application.Queries.LoyaltyProgram;
using BHS.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BHS.API.Controllers;

public class LoyaltyProgramController : BaseController
{
    private readonly ILoyaltyProgramQuery _loyaltyProgramQuery;
    private readonly IMediator _mediator;

    public LoyaltyProgramController(IMediator mediator, ILoyaltyProgramQuery loyaltyProgramQuery)
    {
        _mediator = mediator;
        _loyaltyProgramQuery = loyaltyProgramQuery;
    }

    #region Put

    /// <summary>
    ///     Cập nhật hình ảnh cho chương trình tích điểm/đổi thưởng
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> AddBannerAccumulatePoint([FromForm] AddBannerLoyaltyProgram request)
    {
        return Ok(await _mediator.Send(request));
    }

    #endregion

    #region Get

    /// <summary>
    ///     Lấy chi tiết chương trình tích điểm/đổi quà
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _loyaltyProgramQuery.GetAsync(id));
    }

    /// <summary>
    ///     Lấy tất cả chương trình tích điểm/đổi quà
    /// </summary>
    /// <param name="queryTemplate"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryTemplate queryTemplate)
    {
        return Ok(await _loyaltyProgramQuery.GetAllAsync(queryTemplate));
    }

    /// <summary>
    ///     Lấy tất cả phần quà từ các chương trình đổi quà của vendor đang chọn
    /// </summary>
    /// <param name="queryTemplate"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("Gift")]
    public async Task<IActionResult> GetAllGift([FromQuery] QueryTemplate queryTemplate)
    {
        return Ok(await _loyaltyProgramQuery.GetAllGiftAsync(queryTemplate));
    }

    /// <summary>
    ///     Tìm kiếm chương trình tích điểm/đổi quà
    /// </summary>
    /// <param name="name"></param>
    /// <param name="queryTemplate"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{name}")]
    public async Task<IActionResult> Get(string name, [FromQuery] QueryTemplate queryTemplate)
    {
        var result = await _loyaltyProgramQuery.GetLoyaltyByNameAsync(name, queryTemplate);
        return Ok(result);
    }

    #endregion

    #region Post

    /// <summary>
    ///     Khởi tạo chương trình tích điểm/đổi quà
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLoyaltyProgram request)
    {
        return Ok(await _mediator.Send(request));
    }


    /// <summary>
    ///     Khởi tạo hình ảnh cho chương trình tích điểm/đổi quà
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("LoyaltyProgramImage")]
    public async Task<IActionResult> Create([FromForm] CreateLoyaltyProgramImage request)
    {
        return Ok(await _mediator.Send(request));
    }

    /// <summary>
    ///     Khởi tạo sản phẩm tham gia chương trình tích điểm
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("ProductParticipatingLoyalty")]
    public async Task<IActionResult> Create([FromBody] CreateProductParticipatingLoyalty request)
    {
        return Ok(await _mediator.Send(request));
    }

    /// <summary>
    ///     Khởi tạo barcode của sản phẩm tham gia chương trình tích điểm
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("ProductParticipatingLoyalty/BarCode")]
    public async Task<IActionResult> Create([FromBody] CreateBarCodeOfProductParticipatingLoyalty request)
    {
        return Ok(await _mediator.Send(request));
    }

    /// <summary>
    ///     Khởi tạo quà của chương trình đổi quà
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("GiftOfLoyalty")]
    public async Task<IActionResult> Create([FromBody] CreateGiftOfLoyalty request)
    {
        return Ok(await _mediator.Send(request));
    }

    #endregion
}