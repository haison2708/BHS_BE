using BHS.API.Application.Commands.CartCommand;
using BHS.API.ViewModels.Cart;
using BHS.Domain.Entities.Users;
using BHS.Domain.SeedWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BHS.API.Application.Handlers.CartHandler;

public class UpdateCartHandler : IRequestHandler<UpdateCart, CartViewModel?>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCartHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CartViewModel?> Handle(UpdateCart request, CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork.Repository<Cart>().Get()
            .FirstOrDefaultAsync(x => x.Id == request.CartId, cancellationToken);
        if (cart is null)
            return null;
        cart.Quantity = request.Quantity;
        cart.IsRemove = request.IsRemove;
        return await _unitOfWork.SaveChangesAsync(cancellationToken) ? _mapper.Map<CartViewModel>(cart) : null;
    }
}