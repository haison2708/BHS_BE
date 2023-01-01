using MediatR;

namespace BHS.API.Application.Commands.UserCommand;

public class GiftExchange : IRequest<string?>
{
    public int GiftOfLoyaltyId { get; set; }
    public int Quantity { get; set; }
}