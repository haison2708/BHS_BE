using BHS.API.Application.Commands.FortuneCommand;
using BHS.Domain.Entities.Fortunes;
using BHS.Domain.SeedWork;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BHS.API.CommandValidators.FortunesValidator;

public class AddBannerFortuneValidator : AbstractValidator<AddBannerFortune>
{
    public AddBannerFortuneValidator(IUnitOfWork unitOfWork, IStringLocalizer<CommonValidationLocalization> localizer)
    {
        RuleFor(x => x.FortuneId).Must(id =>
                unitOfWork.Repository<Fortune>().Get().FirstOrDefault(x => x.Id == id) is not null)
            .WithErrorCode(ErrorCode.IdNotExist).WithMessage(localizer["IdNotExist"]);
        RuleFor(x => x.ImageBanner).SetValidator(new FileValidator()!);
    }
}