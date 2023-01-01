using BHS.API.Application.Commands.LoyaltyProgramCommand;
using BHS.API.ViewModels.LoyaltyPrograms;
using BHS.Domain.Entities.LoyaltyPrograms;
using BHS.Domain.Entities.Products;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;
using AutoMapper;
using MediatR;

namespace BHS.API.Application.Handlers.LoyaltyProgramHandler;

public class CreateGiftOfLoyaltyHandler : IRequestHandler<CreateGiftOfLoyalty, GiftOfLoyaltyViewModel?>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGiftOfLoyaltyHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GiftOfLoyaltyViewModel?> Handle(CreateGiftOfLoyalty request, CancellationToken cancellationToken)
    {
        var type = request.Type;
        var giftOfLoyalty = new GiftOfLoyalty
        {
            LoyaltyProgramId = request.LoyaltyProgramId,
            SourceId = Convert.ToInt32(request.SourceId),
            Name = request.Name,
            Limit = request.Limit,
            QtyAvailable = request.Limit,
            Quantity = request.Quantity,
            Point = request.Point,
            Type = type,
            FromDateOfExchange = request.FromDateOfExchange
        };
        giftOfLoyalty.Name = type switch
        {
            GiftType.Product => _unitOfWork.Repository<Product>().Get().FirstOrDefault(x => x.Id == request.SourceId)!
                .Name,
            GiftType.RotationLuck => "lượt quay",
            _ => throw new ArgumentOutOfRangeException()
        };
        await _unitOfWork.Repository<GiftOfLoyalty>().InsertAsync(giftOfLoyalty);

        return await _unitOfWork.SaveChangesAsync(cancellationToken)
            ? _mapper.Map<GiftOfLoyaltyViewModel>(giftOfLoyalty)
            : null;
    }
}