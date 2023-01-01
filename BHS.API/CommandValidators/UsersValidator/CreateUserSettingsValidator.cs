using BHS.API.Application.Commands.UserCommand;
using BHS.Domain.Entities.Languages;
using BHS.Domain.Entities.Vendors;
using BHS.Domain.SeedWork;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BHS.API.CommandValidators.UsersValidator;

public class CreateUserSettingsValidator : AbstractValidator<CreateUserSettings>
{
    public CreateUserSettingsValidator(IUnitOfWork unitOfWork, IStringLocalizer<CommonValidationLocalization> localizer)
    {
        RuleFor(x => x.LangId)
            .Must(langId => unitOfWork.Repository<Language>().Get().FirstOrDefault(x => x.Id == langId) is not null)
            .WithErrorCode(ErrorCode.IdNotExist)
            .WithMessage(localizer["IdNotExist"]);
        When(x => x.VendorId is not null, () =>
        {
            RuleFor(x => x.VendorId)
                .Must(vendorId =>
                    unitOfWork.Repository<Vendor>().Get().FirstOrDefault(x => x.Id == vendorId) is not null)
                .WithErrorCode(ErrorCode.IdNotExist)
                .WithMessage(localizer["IdNotExist"]);
        });
    }
}