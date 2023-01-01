using BHS.API.Application.Commands.NotifyCommand;
using BHS.Domain.Entities.Vendors;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BHS.API.CommandValidators.NotifyValidator;

public class CreateNotificationSetUpValidator : AbstractValidator<CreateNotificationSetUp>
{
    public CreateNotificationSetUpValidator(IUnitOfWork unitOfWork,
        IStringLocalizer<CommonValidationLocalization> localizer)
    {
        When(x => x.VendorId is not null,
            () =>
            {
                RuleFor(x => x.VendorId).Must(id =>
                        unitOfWork.Repository<Vendor>().Get().FirstOrDefault(x => x.Id == id) is not null)
                    .WithErrorCode(ErrorCode.IdNotExist).WithMessage(localizer["IdNotExist"]);
            });
        RuleFor(x => x.Type).Must(type => Enum.IsDefined(typeof(NotifyType), type))
            .WithErrorCode(ErrorCode.IncorrectType).WithMessage(localizer["MustCorrectType"]);
    }
}