using BHS.API.ViewModels.Users;
using MediatR;

namespace BHS.API.Application.Commands.UserCommand;

public class CreateUserAppToken : IRequest<UserAppTokenViewModel>
{
    public string? AppId { get; set; }
    public string? Token { get; set; }
    public int Environment { get; set; }
}