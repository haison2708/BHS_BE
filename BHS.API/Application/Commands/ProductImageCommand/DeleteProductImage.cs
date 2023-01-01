using MediatR;

namespace BHS.API.Application.Commands.ProductImageCommand;

public class DeleteProductImage : IRequest<bool>
{
    public DeleteProductImage(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}