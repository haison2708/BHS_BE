using MediatR;

namespace BHS.API.Application.Commands.ProductCommand;

public class CreateProductForUser : IRequest<bool>
{
    public int ProductId { get; set; }
}