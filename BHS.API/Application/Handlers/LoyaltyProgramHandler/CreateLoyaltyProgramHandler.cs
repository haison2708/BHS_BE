using BHS.API.Application.Commands.LoyaltyProgramCommand;
using BHS.API.Application.Commands.NotifyCommand;
using BHS.API.Services;
using BHS.API.ViewModels.LoyaltyPrograms;
using BHS.API.ViewModels.Vendor;
using BHS.Domain.Entities.LoyaltyPrograms;
using BHS.Domain.Entities.Vendors;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BHS.API.Application.Handlers.LoyaltyProgramHandler;

public class CreateLoyaltyProgramHandler : IRequestHandler<CreateLoyaltyProgram, LoyaltyProgramViewModel?>
{
    private readonly IBackgroundJob _backgroundJob;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLoyaltyProgramHandler(IMapper mapper, IUnitOfWork unitOfWork, IBackgroundJob backgroundJob,
        IMediator mediator)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _backgroundJob = backgroundJob;
        _mediator = mediator;
    }

    public async Task<LoyaltyProgramViewModel?> Handle(CreateLoyaltyProgram request,
        CancellationToken cancellationToken)
    {
        var startDate = request.StartDate.UtcDateTime;
        var endDate = request.EndDate.UtcDateTime;
        var expirationDate = request.ExpirationDate.UtcDateTime;
        var type = request.Type;
        var loyaltyProgram = new LoyaltyProgram
        {
            VendorId = request.VendorId,
            Name = request.Name,
            StartDate = startDate,
            EndDate = endDate,
            ExpirationDate = expirationDate,
            Status = CommonStatus.Active,
            Type = type
        };
        var newLoyaltyProgram = await _unitOfWork.Repository<LoyaltyProgram>().InsertAsync(loyaltyProgram);
        var isSave = await _unitOfWork.SaveChangesAsync(cancellationToken);
        string content;
        string remark;
        switch (type)
        {
            case LoyaltyProgramType.GiftExchange:
                content = "Tích điểm từ nhà cung cấp sau đó đổi các phần quà từ chương trình";
                remark = $"/gift-exchange-program-detail/{loyaltyProgram.Id}/{loyaltyProgram.Type}";
                break;
            case LoyaltyProgramType.QrCode:
                content = "Tích điểm bằng cách quét QR Code trên các sản phẩm";
                remark = $"/accumulate-points-program-detail/{loyaltyProgram.Id}/{loyaltyProgram.Type}";
                break;
            case LoyaltyProgramType.Purchase:
                content = "Tích điểm khi mua sản phẩm";
                remark = $"/accumulate-points-program-detail/{loyaltyProgram.Id}/{loyaltyProgram.Type}";
                break;
            default:
                content = "";
                remark = "";
                break;
        }

        var newNotify = new CreateNotificationSetUp
        {
            Title = request.Name,
            SubTitle =
                $"Từ {startDate.ToString(FormatDate.FormatDateDdMmYyyy)} - {endDate.ToString(FormatDate.FormatDateDdMmYyyy)}",
            Type = NotifyType.Loyalty,
            TimeStart = startDate,
            Content = content,
            VendorId = request.VendorId,
            Remark = remark,
            ToCurrentUser = false
        };
        await _mediator.Send(newNotify, cancellationToken);
        if (request.Type != LoyaltyProgramType.GiftExchange)
            _backgroundJob.ScheduleUpdatePointAsync("LoyaltyProgram", newLoyaltyProgram.Id,
                request.ExpirationDate > request.EndDate
                    ? request.ExpirationDate
                    : request.EndDate);
        var result = _mapper.Map<LoyaltyProgramViewModel>(newLoyaltyProgram);
        result.TypeName = type == LoyaltyProgramType.GiftExchange ? "Đổi quà" : "Tích điểm";
        result.Vendor = _mapper.Map<VendorViewModel>(await _unitOfWork.Repository<Vendor>().Get()
            .FirstOrDefaultAsync(x => x.Id == request.VendorId, cancellationToken));
        return isSave ? result : null;
    }
}