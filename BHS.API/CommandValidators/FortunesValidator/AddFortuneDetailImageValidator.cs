using BHS.API.Application.Commands.FortuneCommand;
using BHS.Domain.Entities.Fortunes;
using BHS.Domain.SeedWork;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BHS.API.CommandValidators.FortunesValidator;

public class AddFortuneDetailImageValidator : AbstractValidator<AddFortuneDetailImage>
{
    public AddFortuneDetailImageValidator(IUnitOfWork unitOfWork,
        IStringLocalizer<CommonValidationLocalization> localizer)
    {
        RuleFor(x => x.FortuneDetailId)
            .Must(id => unitOfWork.Repository<FortuneDetail>().Get().FirstOrDefault(x => x.Id == id) is not null)
            .WithErrorCode(ErrorCode.IdNotExist)
            .WithMessage(localizer["IdNotExist"]);
        RuleFor(x => x.Image).SetValidator(new FileValidator()!);
    }
}