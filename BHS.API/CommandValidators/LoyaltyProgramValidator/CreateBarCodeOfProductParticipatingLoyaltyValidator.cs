using BHS.API.Application.Commands.LoyaltyProgramCommand;
using BHS.Domain.Entities.LoyaltyPrograms;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BHS.API.CommandValidators.LoyaltyProgramValidator;

public class
    CreateBarCodeOfProductParticipatingLoyaltyValidator : AbstractValidator<CreateBarCodeOfProductParticipatingLoyalty>
{
    public CreateBarCodeOfProductParticipatingLoyaltyValidator(IUnitOfWork unitOfWork,
        IStringLocalizer<CommonValidationLocalization> localizer)
    {
        RuleFor(x => x).Must(x =>
            {
                var product = unitOfWork.Repository<ProductParticipatingLoyalty>().Get().FirstOrDefault(p
                    => p.Id == x.ProductId && p.Type == LoyaltyProgramType.QrCode);
                return product != null;
            }).WithErrorCode(ErrorCode.IdNotExist)
            .WithMessage(localizer["IdNotExist"]);
    }
}