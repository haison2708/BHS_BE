using BHS.API.Application.Commands.CartCommand;
using BHS.API.Services;
using BHS.API.ViewModels.Cart;
using BHS.Domain.Entities.Users;
using BHS.Domain.SeedWork;
using AutoMapper;
using MediatR;

namespace BHS.API.Application.Handlers.CartHandler;

public class CreatCartHandler : IRequestHandler<CreateCart, CartViewModel?>
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreatCartHandler(IUnitOfWork unitOfWork, IIdentityService identityService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _identityService = identityService;
        _mapper = mapper;
    }

    public async Task<CartViewModel?> Handle(CreateCart request, CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork.Repository<Cart>().InsertAsync(new Cart
        {
            ProductId = request.ProductId,
            VendorId = _identityService.GetCurrentVendorId(),
            UserId = _identityService.GetUserIdentity(),
            IsOrder = false,
            IsRemove = false,
            Quantity = request.Quantity
        });
        return await _unitOfWork.SaveChangesAsync(cancellationToken) ? _mapper.Map<CartViewModel>(cart) : null;
    }
}