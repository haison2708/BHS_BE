using BHS.API.Application.Commands.CartCommand;
using BHS.API.Application.Commands.UserCommand;
using BHS.API.Application.Queries.User;
using BHS.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BHS.API.Controllers;

public class UserController : BaseController
{
    private readonly IStringLocalizer<CommonValidationLocalization> _localizer;
    private readonly IMediator _mediator;
    private readonly IUserQuery _userQuery;

    public UserController(IMediator mediator, IUserQuery userQuery,
        IStringLocalizer<CommonValidationLocalization> localizer)
    {
        _mediator = mediator;
        _userQuery = userQuery;
        _localizer = localizer;
    }

    #region Get

    /// <summary>
    ///     Lấy lịch sử tích điểm
    /// </summary>
    /// <param name="queryTemplate"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("PointOfUser/History")]
    public async Task<IActionResult> GetHistoryPoint([FromQuery] QueryTemplate queryTemplate)
    {
        return Ok(await _userQuery.GetHistoriesPointsAsync(queryTemplate));
    }

    /// <summary>
    ///     Lấy tổng điểm của các chương trình
    /// </summary>
    /// <param name="queryTemplate"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("PointOfUser/Programs")]
    public async Task<IActionResult> GetTotalPoint([FromQuery] QueryTemplate queryTemplate)
    {
        return Ok(await _userQuery.GetTotalPointsOfProgramsAsync(queryTemplate));
    }

    /// <summary>
    ///     Lấy tất cả giỏ hàng
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("Cart")]
    public async Task<IActionResult> GetListCart()
    {
        return Ok(await _userQuery.GetCartsAsync());
    }

    /// <summary>
    ///     Lấy quà
    /// </summary>
    /// <param name="type"></param>
    /// <param name="queryTemplate"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("Gift")]
    public async Task<IActionResult> GetGift(int type, [FromQuery] QueryTemplate queryTemplate)
    {
        return Ok(await _userQuery.GetGiftsByTypeAsync(type, queryTemplate));
    }

    /// <summary>
    ///     Lấy chi tiết quà
    /// </summary>
    /// <param name="giftId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("Gift/{giftId:int}")]
    public async Task<IActionResult> GetGiftById(int giftId)
    {
        return Ok(await _userQuery.GetGiftAsync(giftId));
    }

    /// <summary>
    ///     Lấy lịch sử quay theo chương trình vòng quay
    /// </summary>
    /// <param name="fortuneId"></param>
    /// <param name="queryTemplate"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("Fortune/{fortuneId:int}/FortuneUserReward")]
    public async Task<IActionResult> GetHistoryFortune(int fortuneId, [FromQuery] QueryTemplate queryTemplate)
    {
        return Ok(await _userQuery.GetHistoriesFortuneAsync(fortuneId, queryTemplate));
    }

    /// <summary>
    ///     Lấy tổng điểm tích lũy và tổng quà
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("TotalPoints/TotalGift")]
    public async Task<IActionResult> GetTotalPointsAndGift()
    {
        return Ok(await _userQuery.GetTotalPointsAndGiftsAsync());
    }

    /// <summary>
    ///     Lấy settings của user
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("Settings")]
    public async Task<IActionResult> GetUserSettings()
    {
        return Ok(await _userQuery.GetUserSettingsAsync());
    }

    /// <summary>
    ///     Lấy tổng quan các vendor
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("Vendor/Overview")]
    public async Task<IActionResult> VendorOverview()
    {
        return Ok(await _userQuery.VendorOverview());
    }

    #endregion

    #region Post

    /// <summary>
    ///     Khởi tạo giỏ hàng
    /// </summary>
    /// <param name="createCart"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Cart")]
    public async Task<IActionResult> Create([FromBody] CreateCart createCart)
    {
        return Ok(await _mediator.Send(createCart));
    }

    /// <summary>
    ///     Khởi tạo user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUser user)
    {
        return Ok(await _mediator.Send(user));
    }

    /// <summary>
    /// </summary>
    /// <param name="userFollowVendor"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("UserFollowVendor")]
    public async Task<IActionResult> Create([FromBody] CreateUserFollowVendor userFollowVendor)
    {
        return Ok(await _mediator.Send(userFollowVendor));
    }

    /// <summary>
    ///     Quay thưởng
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("FortuneUserReward")]
    public async Task<IActionResult> Create([FromBody] CreateFortuneUserReward request)
    {
        return Ok(await _mediator.Send(request));
    }

    /// <summary>
    ///     Tạo token FCM
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("AppToken")]
    public async Task<IActionResult> Create([FromBody] CreateUserAppToken request)
    {
        return Ok(await _mediator.Send(request));
    }

    #endregion

    #region Put

    /// <summary>
    ///     Sử dụng Qrcode
    /// </summary>
    /// <param name="useQrCode"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("UseQrCode")]
    public async Task<IActionResult> UseQrCode([FromBody] UseQrCode useQrCode)
    {
        return Ok(await _mediator.Send(useQrCode));
    }

    /// <summary>
    ///     Cập nhật giỏ hàng
    /// </summary>
    /// <param name="updateCart"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Cart")]
    public async Task<IActionResult> Update([FromBody] UpdateCart updateCart)
    {
        return Ok(await _mediator.Send(updateCart));
    }

    /// <summary>
    ///     Đăng xuất
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("Logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    /// <summary>
    ///     Đổi quà
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("GiftExchange")]
    public async Task<IActionResult> GiftExchange([FromBody] GiftExchange request)
    {
        var result = await _mediator.Send(request);
        return result is null ? Ok() : BadRequest(_localizer[result]);
    }

    /// <summary>
    ///     Cập nhật settings
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("UserSettings")]
    public async Task<IActionResult> UserSettings([FromBody] CreateUserSettings request)
    {
        return Ok(await _mediator.Send(request));
    }

    #endregion
}