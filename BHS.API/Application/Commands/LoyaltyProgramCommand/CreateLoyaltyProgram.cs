using BHS.API.ViewModels.LoyaltyPrograms;
using BHS.Domain.Enumerate;
using MediatR;

namespace BHS.API.Application.Commands.LoyaltyProgramCommand;

public class CreateLoyaltyProgram : IRequest<LoyaltyProgramViewModel>
{
    public int VendorId { get; set; }
    public string? Name { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public DateTimeOffset ExpirationDate { get; set; }
    public LoyaltyProgramType Type { get; set; }
}