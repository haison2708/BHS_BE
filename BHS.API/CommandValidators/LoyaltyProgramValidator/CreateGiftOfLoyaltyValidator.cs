using BHS.API.Application.Commands.LoyaltyProgramCommand;
using BHS.Domain.Entities.Fortunes;
using BHS.Domain.Entities.LoyaltyPrograms;
using BHS.Domain.Entities.Products;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BHS.API.CommandValidators.LoyaltyProgramValidator;

public class CreateGiftOfLoyaltyValidator : AbstractValidator<CreateGiftOfLoyalty>
{
    public CreateGiftOfLoyaltyValidator(IUnitOfWork unitOfWork,
        IStringLocalizer<CommonValidationLocalization> localizer)
    {
        RuleFor(x => x.LoyaltyProgramId).Must(loyaltyProgramId =>
                unitOfWork.Repository<LoyaltyProgram>().Get().FirstOrDefault(x => x.Id == loyaltyProgramId) is not null)
            .WithErrorCode(ErrorCode.IdNotExist).WithMessage(localizer["IdNotExist"]);
        RuleFor(x => x.Type).Must(type => Enum.IsDefined(typeof(GiftType), type)).WithErrorCode(ErrorCode.IncorrectType)
            .WithMessage(localizer["MustCorrectType"]);
        When(x => x.Type == GiftType.Voucher, () =>
        {
            RuleFor(x => x.Name).NotEmpty().NotNull()
                .WithErrorCode(ErrorCode.NullOrEmpty).WithMessage(localizer["MustNotNullAndNotEmpty"]);
        });
        When(x => x.Type == GiftType.Product, () =>
        {
            RuleFor(x => x.SourceId).Must(sourceId =>
                    unitOfWork.Repository<Product>().Get()
                        .FirstOrDefault(x => x.Id == Convert.ToInt32(sourceId)) is not null)
                .WithErrorCode(ErrorCode.IdNotExist).WithMessage(localizer["IdNotExist"]);
        });
        When(x => x.Type == GiftType.RotationLuck, () =>
        {
            RuleFor(x => x.SourceId).Must(sourceId =>
                    unitOfWork.Repository<Fortune>().Get()
                        .FirstOrDefault(x => x.Id == Convert.ToInt32(sourceId)) is not null)
                .WithErrorCode(ErrorCode.IdNotExist).WithMessage(localizer["IdNotExist"]);
        });

        RuleFor(x => x.Limit).GreaterThan(0).WithErrorCode(ErrorCode.LessThanValue)
            .WithMessage(x => localizer["MustGreaterThanOneParam", "0"]);
        RuleFor(x => x.Quantity).GreaterThan(0).WithErrorCode(ErrorCode.LessThanValue)
            .WithMessage(x => localizer["MustGreaterThanOneParam", "0"]);
        RuleFor(x => x.Point).GreaterThan(0).WithErrorCode(ErrorCode.LessThanValue)
            .WithMessage(x => localizer["MustGreaterThanOneParam", "0"]);
    }
}