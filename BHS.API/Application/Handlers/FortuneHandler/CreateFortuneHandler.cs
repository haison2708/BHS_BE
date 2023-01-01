using BHS.API.Application.Commands.FortuneCommand;
using BHS.API.Services;
using BHS.API.ViewModels.Fortunes;
using BHS.Domain.Entities.Fortunes;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;
using AutoMapper;
using MediatR;

namespace BHS.API.Application.Handlers.FortuneHandler;

public class CreateFortuneHandler : IRequestHandler<CreateFortune, FortuneViewModel?>
{
    private readonly IBackgroundJob _backgroundJob;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateFortuneHandler(IUnitOfWork unitOfWork, IMapper mapper, IBackgroundJob backgroundJob)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _backgroundJob = backgroundJob;
    }

    public async Task<FortuneViewModel?> Handle(CreateFortune request, CancellationToken cancellationToken)
    {
        var fromDate = request.FromDate.UtcDateTime;
        var toDate = request.ToDate.UtcDateTime;
        var fortune = new Fortune
        {
            VendorId = request.VendorId,
            Descr = request.Descr,
            FromDate = fromDate,
            ToDate = toDate,
            Status = CommonStatus.Active,
            FortuneDetails = new List<FortuneDetail>
            {
                new()
                {
                    Descr = "Chúc may mắn lần sau",
                    Image = "",
                    Probability = 100,
                    Limit = 0,
                    QtyAvailable = 0,
                    FortuneType = FortuneType.FortuneBetterLuckNextTime,
                    Quantity = 0
                }
            }
        };
        var newFortune = await _unitOfWork.Repository<Fortune>().InsertAsync(fortune);
        var isSaved = await _unitOfWork.SaveChangesAsync(cancellationToken);
        _backgroundJob.ScheduleUpdatePointAsync("Fortune", newFortune.Id, request.ToDate);
        return isSaved ? _mapper.Map<FortuneViewModel>(newFortune) : null;
    }
}