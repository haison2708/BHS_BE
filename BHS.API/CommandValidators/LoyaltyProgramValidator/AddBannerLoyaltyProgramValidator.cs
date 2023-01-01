using BHS.API.Application.Commands.LoyaltyProgramCommand;
using BHS.Domain.Entities.LoyaltyPrograms;
using BHS.Domain.SeedWork;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BHS.API.CommandValidators.LoyaltyProgramValidator;

public class AddBannerLoyaltyProgramValidator : AbstractValidator<AddBannerLoyaltyProgram>
{
    public AddBannerLoyaltyProgramValidator(IUnitOfWork unitOfWork,
        IStringLocalizer<CommonValidationLocalization> localizer)
    {
        RuleFor(x => x.LoyaltyProgramId).Must(id =>
                unitOfWork.Repository<LoyaltyProgram>().Get().FirstOrDefault(x => x.Id == id) is not null)
            .WithErrorCode(ErrorCode.IdNotExist).WithMessage(localizer["IdNotExist"]);
        RuleFor(x => x.ImageBanner).SetValidator(new FileValidator()!);
    }
}