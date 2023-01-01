using BHS.API.Application.Commands.UserCommand;
using BHS.Domain.Entities.Vendors;
using BHS.Domain.SeedWork;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BHS.API.CommandValidators.UsersValidator;

public class CreateUserFollowVendorValidator : AbstractValidator<CreateUserFollowVendor>
{
    public CreateUserFollowVendorValidator(IUnitOfWork unitOfWork,
        IStringLocalizer<CommonValidationLocalization> localizer)
    {
        RuleFor(x => x.VendorIds).Must(vendorIds =>
        {
            if (vendorIds is null)
                return false;
            var listVendorId = vendorIds.Split(",");
            var vendors = unitOfWork.Repository<Vendor>().Get().Where(x => listVendorId.Any(v => v == x.Id.ToString()));
            return vendors.Count() == vendorIds.Split(",").Length;
        }).WithErrorCode(ErrorCode.IdNotExist).WithMessage(localizer["IdNotExist"]);
    }
}