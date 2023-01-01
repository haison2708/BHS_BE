using MediatR;

namespace BHS.API.Application.Commands.CategoryCommand;

public class DeleteCategory : IRequest<bool>
{
    public int Id;

    public DeleteCategory(int id)
    {
        Id = id;
    }
}