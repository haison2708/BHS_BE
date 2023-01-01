using BHS.API.Application.Commands.FortuneCommand;
using BHS.API.Services;
using BHS.Domain.Entities.Vendors;
using BHS.Domain.SeedWork;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BHS.API.CommandValidators.FortunesValidator;

public class FortuneValidator : AbstractValidator<CreateFortune>
{
    public FortuneValidator(IUnitOfWork unitOfWork, IStringLocalizer<CommonValidationLocalization> localizer,
        IIdentityService identityService)
    {
        RuleFor(x => x.VendorId).Must(vendorId =>
                unitOfWork.Repository<Vendor>().Get().FirstOrDefault(x => x.Id == vendorId) is not null)
            .WithErrorCode(ErrorCode.IdNotExist).WithMessage(localizer["IdNotExist"]);
        RuleFor(x => x.Descr).NotEmpty().NotNull().WithErrorCode(ErrorCode.NullOrEmpty)
            .WithMessage(localizer["MustNotNullAndNotEmpty"]);
        RuleFor(x => x.FromDate).Must(fromDate => fromDate > DateTimeOffset.UtcNow)
            .WithErrorCode(ErrorCode.LessThanValue)
            .WithMessage(x => localizer["MustGreaterThanTwoParams", nameof(x.FromDate), localizer["CurrentTime"]]);
        RuleFor(x => new { x.FromDate, x.ToDate }).Must(x => x.ToDate > x.FromDate)
            .WithName(x => $"{nameof(x.FromDate)}&{nameof(x.ToDate)}").WithErrorCode(ErrorCode.LessThanValue)
            .WithMessage(x => localizer["MustGreaterThanTwoParams", nameof(x.ToDate), nameof(x.FromDate)]);
    }
}