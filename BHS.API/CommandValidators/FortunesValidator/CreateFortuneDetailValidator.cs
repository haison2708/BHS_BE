using BHS.API.Application.Commands.FortuneCommand;
using BHS.Domain.Entities.Fortunes;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BHS.API.CommandValidators.FortunesValidator;

public class CreateFortuneDetailValidator : AbstractValidator<CreateFortuneDetail>
{
    public CreateFortuneDetailValidator(IUnitOfWork unitOfWork,
        IStringLocalizer<CommonValidationLocalization> localizer)
    {
        RuleFor(x => x.FortuneId)
            .Must(fortuneId =>
                unitOfWork.Repository<Fortune>().Get().FirstOrDefault(x => x.Id == fortuneId) is not null)
            .WithErrorCode(ErrorCode.IdNotExist).WithMessage(localizer["IdNotExist"]);
        RuleFor(x => x.FortuneType).Must(fortuneType =>
                fortuneType is FortuneType.FortunePoints or FortuneType.FortuneTurns or FortuneType.FortuneGift)
            .WithErrorCode(ErrorCode.IncorrectType)
            .WithMessage(localizer["MustCorrectType"]);
        RuleFor(x => new { x.Probability, x.FortuneId }).Must(x =>
            {
                var fortune = unitOfWork.Repository<Fortune>().Get().Include(f => f.FortuneDetails)
                    .FirstOrDefault(f => f.Id == x.FortuneId);
                if (fortune!.FortuneDetails is not null && fortune.FortuneDetails.Any())
                    return x.Probability > 0 && 100 - fortune.FortuneDetails.Sum(f => f.Probability) >= x.Probability;
                return x.Probability > 0;
            }).WithErrorCode(ErrorCode.LessThanValue)
            .WithMessage(localizer["MustGreaterThanOneParam", "0"]);
        RuleFor(x => x.Limit).GreaterThan(0)
            .WithErrorCode(ErrorCode.LessThanValue)
            .WithMessage(localizer["MustGreaterThanOneParam", "0"]);
        RuleFor(x => x.Quantity).GreaterThan(0)
            .WithErrorCode(ErrorCode.LessThanValue)
            .WithMessage(localizer["MustGreaterThanOneParam", "0"]);
    }
}