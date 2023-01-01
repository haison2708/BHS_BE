using MediatR;

namespace BHS.API.Application.Commands.NotifyCommand;

public class UpdateNotify : IRequest
{
    public int? NotifyId { get; set; }
}