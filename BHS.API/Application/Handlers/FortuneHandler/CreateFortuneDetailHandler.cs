using BHS.API.Application.Commands.FortuneCommand;
using BHS.API.ViewModels.Fortunes;
using BHS.Domain.Entities.Fortunes;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BHS.API.Application.Handlers.FortuneHandler;

public class CreateFortuneItemHandler : IRequestHandler<CreateFortuneDetail, FortuneDetailViewModel?>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateFortuneItemHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<FortuneDetailViewModel?> Handle(CreateFortuneDetail request, CancellationToken cancellationToken)
    {
        var fortuneDetails = await _unitOfWork.Repository<FortuneDetail>().Get()
            .Where(
                x => x.FortuneType == FortuneType.FortuneBetterLuckNextTime && x.FortuneId == request.FortuneId)
            .ToListAsync(cancellationToken);
        if (fortuneDetails.Any())
        {
            var fortuneBetterLuckNextTime =
                fortuneDetails.FirstOrDefault(x => x.FortuneType == FortuneType.FortuneBetterLuckNextTime);
            if (fortuneBetterLuckNextTime is not null)
                fortuneBetterLuckNextTime.Probability = 100 - request.Probability - fortuneDetails
                    .Where(x => x.FortuneType != FortuneType.FortuneBetterLuckNextTime).Sum(x => x.Probability);
        }

        var fortuneDetail = _mapper.Map<FortuneDetail>(request);
        fortuneDetail.QtyAvailable = fortuneDetail.Limit;
        var type = request.FortuneType;
        fortuneDetail.Descr = type switch
        {
            FortuneType.FortunePoints => "điểm",
            FortuneType.FortuneTurns => "lượt quay",
            _ => throw new ArgumentOutOfRangeException()
        };
        var newFortuneDetail = await _unitOfWork.Repository<FortuneDetail>().InsertAsync(fortuneDetail);
        return await _unitOfWork.SaveChangesAsync(cancellationToken)
            ? _mapper.Map<FortuneDetailViewModel>(newFortuneDetail)
            : null;
    }
}