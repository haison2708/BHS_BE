using BHS.API.ViewModels.LoyaltyPrograms;
using MediatR;

namespace BHS.API.Application.Commands.LoyaltyProgramCommand;

public class CreateProductParticipatingLoyalty : IRequest<ProductParticipatingLoyaltyViewModel>
{
    public int ProductId { get; set; }
    public int LoyaltyProgramId { get; set; }
    public int Points { get; set; }
    public int AmountOfMoney { get; set; }
}