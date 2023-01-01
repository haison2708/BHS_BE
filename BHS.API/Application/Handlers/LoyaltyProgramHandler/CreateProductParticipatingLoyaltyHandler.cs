using BHS.API.Application.Commands.LoyaltyProgramCommand;
using BHS.API.ViewModels.LoyaltyPrograms;
using BHS.Domain.Entities.LoyaltyPrograms;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BHS.API.Application.Handlers.LoyaltyProgramHandler;

public class
    CreateProductParticipatingLoyaltyHandler : IRequestHandler<CreateProductParticipatingLoyalty,
        ProductParticipatingLoyaltyViewModel>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductParticipatingLoyaltyHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductParticipatingLoyaltyViewModel> Handle(CreateProductParticipatingLoyalty request,
        CancellationToken cancellationToken)
    {
        var loyaltyProgram = await _unitOfWork.Repository<LoyaltyProgram>().Get()
            .FirstOrDefaultAsync(x => x.Id == request.LoyaltyProgramId, cancellationToken);
        if (loyaltyProgram is null)
            return null!;
        var productParticipatingAccumulatingPoint = new ProductParticipatingLoyalty
        {
            ProductId = request.ProductId,
            LoyaltyProgramId = request.LoyaltyProgramId,
            Status = CommonStatus.Active,
            Points = request.Points,
            Type = loyaltyProgram.Type,
            AmountOfMoney = request.AmountOfMoney
        };
        var newProductParticipatingLoyalty = await _unitOfWork.Repository<ProductParticipatingLoyalty>()
            .InsertAsync(productParticipatingAccumulatingPoint);
        var isSaved = await _unitOfWork.SaveChangesAsync(cancellationToken);
        var result = _mapper.Map<ProductParticipatingLoyaltyViewModel>(newProductParticipatingLoyalty);
        return (isSaved ? result : null)!;
    }
}