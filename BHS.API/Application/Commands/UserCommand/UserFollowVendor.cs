using MediatR;

namespace BHS.API.Application.Commands.UserCommand;

public class CreateUserFollowVendor : IRequest<bool>
{
    public string? VendorIds { get; set; }
    public bool IsFollow { get; set; }
}