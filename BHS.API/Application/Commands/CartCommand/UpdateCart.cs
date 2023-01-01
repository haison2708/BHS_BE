using BHS.API.ViewModels.Cart;
using MediatR;

namespace BHS.API.Application.Commands.CartCommand;

public class UpdateCart : IRequest<CartViewModel>
{
    public int CartId { get; set; }
    public int Quantity { get; set; }
    public bool IsRemove { get; set; }
}