using BHS.Domain.Entities.Users;
using MediatR;

namespace BHS.API.Application.Commands.UserCommand;

public class CreateUser : IRequest<User>
{
    public string? Identity { get; set; }
    public string? DisplayName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public DateTime Birthday { get; set; }
    public short Gender { get; set; }
    public string? Image { get; set; }
    public short Status { get; set; }
}