using BHS.API.Application.Commands.NotifyCommand;
using BHS.API.Application.Queries.Notify;
using BHS.API.ViewModels;
using BHS.Domain.Enumerate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BHS.API.Controllers;

public class NotifyController : BaseController
{
    private readonly IMediator _mediator;
    private readonly INotifyQuery _notification;

    public NotifyController(IMediator mediator, INotifyQuery notification)
    {
        _mediator = mediator;
        _notification = notification;
    }

    /// <summary>
    ///     Lấy thông báo theo loại
    /// </summary>
    /// <param name="type"></param>
    /// <param name="queryTemplate"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllNotifyMessage(NotifyType type, [FromQuery] QueryTemplate queryTemplate)
    {
        var result = await _notification.GetAllNotifyMessageAsync(type, queryTemplate);
        return Ok(result);
    }

    /// <summary>
    ///     Khởi tạo thông báo
    /// </summary>
    /// <param name="createNotificationSetUp"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("NotificationSetUp")]
    public async Task<IActionResult> Create([FromBody] CreateNotificationSetUp createNotificationSetUp)
    {
        var result = await _mediator.Send(createNotificationSetUp);
        return Ok(result);
    }
}