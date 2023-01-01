using BHS.API.Application.Commands.UserCommand;
using BHS.API.Services;
using BHS.Domain.Entities.Fortunes;
using BHS.Domain.Entities.Users;
using BHS.Domain.SeedWork;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BHS.API.CommandValidators.UsersValidator;

public class CreateFortuneUserRewardValidator : AbstractValidator<CreateFortuneUserReward>
{
    public CreateFortuneUserRewardValidator(IUnitOfWork unitOfWork, IIdentityService identityService,
        IStringLocalizer<CommonValidationLocalization> localizer)
    {
        RuleFor(x => x.FortuneId)
            .Must(fortuneId =>
                unitOfWork.Repository<Fortune>().Get().FirstOrDefault(x => x.Id == fortuneId) is not null)
            .WithErrorCode(ErrorCode.IdNotExist)
            .WithMessage(localizer["IdNotExist"]);
        RuleFor(x => x.FortuneId).Must(fortuneId =>
        {
            var fortune = unitOfWork.Repository<Fortune>().Get().FirstOrDefault(f => f.Id == fortuneId);
            if (fortune is null)
                return false;
            return fortune.ToDate >= DateTime.UtcNow;
        }).WithErrorCode(ErrorCode.Ended).WithMessage(localizer["Ended"]);
        RuleFor(x => x).Must(x => unitOfWork.Repository<FortuneTurnOfUser>().Get().FirstOrDefault(f =>
                f.FortuneId == x.FortuneId && f.UserId == identityService.GetUserIdentity() &&
                f.TurnAvailable > 0) is not null).WithName("User").WithErrorCode(ErrorCode.OutOfTurns)
            .WithMessage(localizer["OutOfTurns"]);
    }
}