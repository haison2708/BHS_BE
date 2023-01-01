using BHS.API.Application.Commands.UserCommand;
using BHS.API.Services;
using BHS.Domain.Entities.LoyaltyPrograms;
using BHS.Domain.Entities.Users;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BHS.API.CommandValidators.UsersValidator;

public class GiftExchangeValidator : AbstractValidator<GiftExchange>
{
    public GiftExchangeValidator(IUnitOfWork unitOfWork, IIdentityService identityService,
        IStringLocalizer<CommonValidationLocalization> localizer)
    {
        RuleFor(x => x.GiftOfLoyaltyId)
            .Must(giftOfLoyaltyId =>
                unitOfWork.Repository<GiftOfLoyalty>().Get().FirstOrDefault(x => x.Id == giftOfLoyaltyId) is not null)
            .WithErrorCode(ErrorCode.IdNotExist)
            .WithMessage(localizer["IdNotExist"]);
        RuleFor(x => x).Must(x =>
        {
            var giftOfLoyalty = unitOfWork.Repository<GiftOfLoyalty>().Get().Include(g => g.LoyaltyProgram)
                .FirstOrDefault(p => p.Id == x.GiftOfLoyaltyId);
            if (giftOfLoyalty is null)
                return false;
            var pointOfUser = unitOfWork.Repository<PointOfUser>().Get().Where(p =>
                p.UserId == identityService.GetUserIdentity()
                && p.VendorId == giftOfLoyalty.LoyaltyProgram!.VendorId &&
                ((p.Type == PointOfUserType.Deducted && p.ProgramType == PointOfUserType.GiftExchange)
                 || p.Type != PointOfUserType.Deducted));
            return pointOfUser.Sum(p => p.Point) >= giftOfLoyalty.Point * x.Quantity;
        }).WithErrorCode(ErrorCode.NotEnoughPoint).WithMessage(localizer["NotEnoughPoint"]);
        RuleFor(x => x.GiftOfLoyaltyId).Must(giftOfLoyaltyId =>
        {
            var giftOfLoyalty = unitOfWork.Repository<GiftOfLoyalty>().Get()
                .FirstOrDefault(x => x.Id == giftOfLoyaltyId);
            if (giftOfLoyalty is null)
                return false;
            return giftOfLoyalty.QtyAvailable > 0;
        }).WithErrorCode(ErrorCode.OutOfGifts).WithMessage(localizer["OutOfGifts"]);
        RuleFor(x => x.GiftOfLoyaltyId).Must(giftOfLoyaltyId =>
        {
            var giftOfLoyalty = unitOfWork.Repository<GiftOfLoyalty>().Get().Include(x => x.LoyaltyProgram)
                .FirstOrDefault(x => x.Id == giftOfLoyaltyId);
            if (giftOfLoyalty is null)
                return false;
            return giftOfLoyalty.LoyaltyProgram!.ExpirationDate > DateTime.UtcNow &&
                   giftOfLoyalty.LoyaltyProgram.EndDate > DateTime.UtcNow;
        }).WithErrorCode(ErrorCode.Ended).WithMessage(localizer["Ended"]);
        RuleFor(x => x).Must(x =>
            {
                var giftOfLoyalty = GetGiftOfLoyalty(unitOfWork, x);
                return x.Quantity > 0 && giftOfLoyalty.QtyAvailable >= x.Quantity;
            }).WithName(x => nameof(x.Quantity)).WithErrorCode(ErrorCode.IncorrectValue)
            .WithMessage(localizer["ValidQuantity"]);
    }

    private static GiftOfLoyalty GetGiftOfLoyalty(IUnitOfWork unitOfWork, GiftExchange giftExchange)
    {
        return unitOfWork.Repository<GiftOfLoyalty>().Get().FirstOrDefault(g => g.Id == giftExchange.GiftOfLoyaltyId)!;
    }
}