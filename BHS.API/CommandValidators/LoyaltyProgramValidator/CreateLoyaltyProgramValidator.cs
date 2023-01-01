using BHS.API.Application.Commands.LoyaltyProgramCommand;
using BHS.Domain.Entities.Vendors;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BHS.API.CommandValidators.LoyaltyProgramValidator;

public class CreateLoyaltyProgramValidator : AbstractValidator<CreateLoyaltyProgram>
{
    public CreateLoyaltyProgramValidator(IUnitOfWork unitOfWork,
        IStringLocalizer<CommonValidationLocalization> localizer)
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Must not null and not empty");
        RuleFor(x => x.StartDate).Must(startDate => startDate > DateTimeOffset.UtcNow)
            .WithErrorCode(ErrorCode.IncorrectFormatDate).WithMessage(x =>
                localizer["MustGreaterThanTwoParams", nameof(x.StartDate), localizer["CurrentTime"]]);
        RuleFor(x => new { x.StartDate, x.EndDate }).Must(x => x.EndDate > x.StartDate).WithName(x =>
                $"{nameof(x.StartDate)}&{nameof(x.EndDate)}").WithErrorCode(ErrorCode.LessThanValue)
            .WithMessage(x => localizer["MustGreaterThanTwoParams", nameof(x.EndDate), nameof(x.StartDate)]);
        When(x => x.Type == LoyaltyProgramType.GiftExchange, () =>
        {
            RuleFor(x => new { x.EndDate, x.ExpirationDate }).Must(x => x.EndDate == x.ExpirationDate)
                .WithErrorCode(ErrorCode.NotEqual)
                .WithName(x => $"{nameof(x.StartDate)}&{nameof(x.EndDate)}")
                .WithMessage(localizer["MustEqual", "EndDate", "ExpirationDate"]);
        }).Otherwise(() =>
        {
            RuleFor(x => new { x.EndDate, x.ExpirationDate }).Must(x => x.ExpirationDate > x.EndDate).WithName(x =>
                    $"{nameof(x.ExpirationDate)}, {nameof(x.EndDate)}").WithErrorCode(ErrorCode.LessThanValue)
                .WithMessage(x => localizer["MustGreaterThanTwoParams", nameof(x.ExpirationDate), nameof(x.EndDate)]);
        });
        RuleFor(x => x.VendorId)
            .Must(id => unitOfWork.Repository<Vendor>().Get().FirstOrDefault(x => x.Id == id) is not null)
            .WithErrorCode(ErrorCode.IdNotExist)
            .WithMessage(localizer["IdNotExist"]);

        RuleFor(x => x.Type).Must(type =>
                type is LoyaltyProgramType.Purchase or LoyaltyProgramType.GiftExchange or LoyaltyProgramType.QrCode)
            .WithErrorCode(ErrorCode.IncorrectType)
            .WithMessage(localizer["MustCorrectType"]);
    }
}