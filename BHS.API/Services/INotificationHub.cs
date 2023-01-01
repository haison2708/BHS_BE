using BHS.API.Application.Commands.NotifyCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BHS.API.Services;

public interface INotificationHub
{
    Task UpdateNotify();
}

[Authorize]
public class NotificationHub : Hub<INotificationHub>
{
    private readonly IIdentityService _identityService;
    private readonly IMediator _mediator;

    public NotificationHub(IMediator mediator, IIdentityService identityService)
    {
        _mediator = mediator;
        _identityService = identityService;
    }

    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, _identityService.GetUserIdentity());
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? ex)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, _identityService.GetUserIdentity());
        await base.OnDisconnectedAsync(ex);
    }

    public async Task SeenNotify(int? notifyId)
    {
        await _mediator.Send(new UpdateNotify
        {
            NotifyId = notifyId
        });
        await Clients.Group(_identityService.GetUserIdentity()).UpdateNotify();
    }
}