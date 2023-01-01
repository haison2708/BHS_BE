using BHS.API.Application.Commands.UserCommand;
using BHS.Domain.Entities.Users;
using BHS.Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BHS.API.Application.Handlers.UserHandler;

public class CreateUserHandler : IRequestHandler<CreateUser, User>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<User> Handle(CreateUser request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Repository<User>().Get()
            .FirstOrDefaultAsync(x => x.Id == request.Identity, cancellationToken);
        var isSaved = true;
        /* Nếu User chưa tồn tại thì thêm vào */
        if (result == null)
        {
            var user = new User
            {
                Id = request.Identity!,
                DisplayName = request.DisplayName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Address = request.Address,
                Birthday = request.Birthday,
                Gender = request.Gender == 1,
                Avatar = request.Image,
                Status = request.Status == 1
            };
            result = await _unitOfWork.Repository<User>().InsertAsync(user);
            isSaved = await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return (isSaved ? result : null)!;
    }
}