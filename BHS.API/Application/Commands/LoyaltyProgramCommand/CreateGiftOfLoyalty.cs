using BHS.API.ViewModels.LoyaltyPrograms;
using BHS.Domain.Enumerate;
using MediatR;

namespace BHS.API.Application.Commands.LoyaltyProgramCommand;

public class CreateGiftOfLoyalty : IRequest<GiftOfLoyaltyViewModel>
{
    public string? Name { get; set; }
    public int? SourceId { get; set; }
    public int LoyaltyProgramId { get; set; }
    public int Quantity { get; set; }
    public int Point { get; set; }
    public GiftType Type { get; set; }
    public int Limit { get; set; }
    public int FromDateOfExchange { get; set; }
}