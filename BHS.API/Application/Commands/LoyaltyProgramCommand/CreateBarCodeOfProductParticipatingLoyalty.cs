using MediatR;

namespace BHS.API.Application.Commands.LoyaltyProgramCommand;

public class CreateBarCodeOfProductParticipatingLoyalty : IRequest<bool>
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}