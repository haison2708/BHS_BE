using MediatR;

namespace BHS.API.Application.Commands.VendorCommand;

public class CreateConfigRankOfVendor : IRequest<object>
{
    public int VendorId { get; set; }
    public string? Name { get; set; }
    public int Points { get; set; }
}