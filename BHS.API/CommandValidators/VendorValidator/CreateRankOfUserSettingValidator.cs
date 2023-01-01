using BHS.API.Application.Commands.VendorCommand;
using BHS.Domain.Entities.Vendors;
using BHS.Domain.SeedWork;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BHS.API.CommandValidators.VendorValidator;

public class CreateRankOfUserSettingValidator : AbstractValidator<CreateConfigRankOfVendor>
{
    public CreateRankOfUserSettingValidator(IUnitOfWork unitOfWork,
        IStringLocalizer<CommonValidationLocalization> localizer)
    {
        RuleFor(x => x.VendorId)
            .Must(vendorId => unitOfWork.Repository<Vendor>().Get().FirstOrDefault(x => x.Id == vendorId) is not null)
            .WithErrorCode(ErrorCode.IdNotExist).WithMessage(localizer["IdNotExist"]);
        RuleFor(x => x.Name).NotEmpty().NotNull().WithErrorCode(ErrorCode.NullOrEmpty)
            .WithMessage(localizer["MustNotNullAndNotEmpty"]);
    }
}