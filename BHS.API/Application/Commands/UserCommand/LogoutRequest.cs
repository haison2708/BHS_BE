using MediatR;

namespace BHS.API.Application.Commands.UserCommand;

public class LogoutRequest : IRequest<bool>
{
    public string? Token { get; set; }
    public string? TokenFcm { get; set; }
}