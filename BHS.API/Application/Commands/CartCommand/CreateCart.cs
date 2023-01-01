using BHS.API.ViewModels.Cart;
using MediatR;

namespace BHS.API.Application.Commands.CartCommand;

public class CreateCart : IRequest<CartViewModel>
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}