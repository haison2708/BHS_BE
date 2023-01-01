using BHS.API.Application.Commands.LoyaltyProgramCommand;
using BHS.Domain.Entities.LoyaltyPrograms;
using BHS.Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BHS.API.Application.Handlers.LoyaltyProgramHandler;

public class
    CreateBarCodeOfProductParticipatingLoyaltyHandler : IRequestHandler<CreateBarCodeOfProductParticipatingLoyalty,
        bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateBarCodeOfProductParticipatingLoyaltyHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(CreateBarCodeOfProductParticipatingLoyalty request,
        CancellationToken cancellationToken)
    {
        var productParticipatingAccumulatingPoint = await _unitOfWork.Repository<ProductParticipatingLoyalty>().Get()
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
        var listBarcode = new List<BarCodeOfProductParticipatingLoyalty>();
        for (var i = 0; i < request.Quantity; i++)
            listBarcode.Add(new BarCodeOfProductParticipatingLoyalty
            {
                ProductParticipating = productParticipatingAccumulatingPoint,
                BarCode = Guid.NewGuid().ToString(),
                IsUsed = false
            });
        await _unitOfWork.Repository<BarCodeOfProductParticipatingLoyalty>()
            .InsertRangeAsync(listBarcode.AsEnumerable());
        return await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}