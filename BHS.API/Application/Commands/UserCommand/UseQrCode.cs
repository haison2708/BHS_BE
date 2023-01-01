using MediatR;

namespace BHS.API.Application.Commands.UserCommand;

public class UseQrCode : IRequest<object>
{
    public string? QrCode { get; set; }
}