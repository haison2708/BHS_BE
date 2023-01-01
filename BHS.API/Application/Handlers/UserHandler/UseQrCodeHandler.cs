using BHS.API.Application.Commands.NotifyCommand;
using BHS.API.Application.Commands.UserCommand;
using BHS.API.Services;
using BHS.Domain.Entities.LoyaltyPrograms;
using BHS.Domain.Entities.Users;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BHS.API.Application.Handlers.UserHandler;

public class UseQrCodeHandler : IRequestHandler<UseQrCode, object>
{
    private readonly IIdentityService _identityService;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public UseQrCodeHandler(IUnitOfWork unitOfWork, IIdentityService identityService, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _identityService = identityService;
        _mediator = mediator;
    }

    public async Task<object> Handle(UseQrCode request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var barCodeOfProduct = await _unitOfWork.Repository<BarCodeOfProductParticipatingLoyalty>().Get()
            .Include(x => x.ProductParticipating).ThenInclude(x => x!.LoyaltyProgram)
            .Include(x => x.ProductParticipating!.Product).FirstOrDefaultAsync(x =>
                x.BarCode == request.QrCode
                && x.ProductParticipating!.Type == LoyaltyProgramType.QrCode, cancellationToken);
        if (barCodeOfProduct is null)
            return null!;
        /* Cập nhật trạng thái thành đã dùng */
        barCodeOfProduct.IsUsed = true;
        /* Cộng điểm cho người dùng */
        await _unitOfWork.Repository<PointOfUser>().InsertAsync(new PointOfUser
        {
            UserId = userId,
            VendorId = barCodeOfProduct.ProductParticipating!.LoyaltyProgram!.VendorId,
            Point = barCodeOfProduct.ProductParticipating.Points,
            Type = PointOfUserType.Added,
            ProgramType = PointOfUserType.Qr,
            //ExpirationDate = barCodeOfProduct.ProductParticipating.LoyaltyProgram.ExpirationDate,
            SourceId = barCodeOfProduct.ProductParticipating.LoyaltyProgramId,
            SourceDetailId = barCodeOfProduct.ProductParticipatingId
        });
        /* Tạo thông báo */
        var newNotify = new CreateNotificationSetUp
        {
            Title = $"+{barCodeOfProduct.ProductParticipating.Points} điểm tích lũy",
            SubTitle = DateTime.UtcNow.ToString(FormatDate.FormatDateDdMmYyyy),
            Type = NotifyType.PointsLoyalty,
            TimeStart = DateTimeOffset.UtcNow,
            Content = $"Quét barcode sản phẩm {barCodeOfProduct.ProductParticipating.Product!.Name}",
            VendorId = barCodeOfProduct.ProductParticipating.LoyaltyProgram.VendorId,
            Remark = "/earn-point-history",
            ToCurrentUser = true
        };
        /* Gọi tới CreateNotificationSetUpHandler */
        await _mediator.Send(newNotify, cancellationToken);

        return (await _unitOfWork.SaveChangesAsync(cancellationToken)
            ? new
            {
                ProductId = barCodeOfProduct.ProductParticipating.Product.Id,
                barCodeOfProduct.ProductParticipating.Product.Name,
                Image = barCodeOfProduct.ProductParticipating.Product.ImgBannerUrl,
                barCodeOfProduct.ProductParticipating.Product.Price,
                barCodeOfProduct.ProductParticipating.Points
            }
            : null)!;
    }
}