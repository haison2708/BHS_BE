using BHS.API.Application.Commands.UserCommand;
using BHS.Domain.Entities.LoyaltyPrograms;
using BHS.Domain.SeedWork;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BHS.API.CommandValidators.UsersValidator;

public class UseQrCodeValidator : AbstractValidator<UseQrCode>
{
    public UseQrCodeValidator(IUnitOfWork unitOfWork, IStringLocalizer<CommonValidationLocalization> localizer)
    {
        RuleFor(x => x.QrCode).Must(barcode =>
        {
            var barCodeOfProduct = unitOfWork.Repository<BarCodeOfProductParticipatingLoyalty>().Get()
                .Include(x => x.ProductParticipating)
                .ThenInclude(x => x!.LoyaltyProgram).FirstOrDefault(x =>
                    x.BarCode == barcode && x.IsUsed == false);
            if (barCodeOfProduct is null)
                return false;
            return barCodeOfProduct.ProductParticipating!.LoyaltyProgram!.ExpirationDate >= DateTime.UtcNow
                   && barCodeOfProduct.ProductParticipating.LoyaltyProgram.EndDate >= DateTime.UtcNow;
        }).WithErrorCode(ErrorCode.NotExistOrUsedOrExpired).WithMessage(localizer["NotExistOrUsedOrExpired"]);
    }
}