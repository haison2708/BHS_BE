using BHS.API.Application.Commands.LoyaltyProgramCommand;
using BHS.Domain.Entities.LoyaltyPrograms;
using BHS.Domain.SeedWork;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BHS.API.CommandValidators.LoyaltyProgramValidator;

public class CreateLoyaltyProgramImageValidator : AbstractValidator<CreateLoyaltyProgramImage>
{
    public CreateLoyaltyProgramImageValidator(IUnitOfWork unitOfWork,
        IStringLocalizer<CommonValidationLocalization> localizer)
    {
        RuleFor(x => x.LoyaltyProgramId)
            .Must(id => unitOfWork.Repository<LoyaltyProgram>().Get().FirstOrDefault(x => x.Id == id) is not null)
            .WithErrorCode(ErrorCode.IdNotExist).WithMessage(localizer["IdNotExist"]);
        RuleForEach(x => x.LoyaltyProgramImages).SetValidator(new FileValidator());
    }
}