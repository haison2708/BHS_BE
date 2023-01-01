using BHS.API.Application.Commands.LoyaltyProgramCommand;
using BHS.Domain.Entities.LoyaltyPrograms;
using BHS.Domain.Entities.Products;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BHS.API.CommandValidators.LoyaltyProgramValidator;

public class CreateProductParticipatingLoyaltyValidator : AbstractValidator<CreateProductParticipatingLoyalty>
{
    public CreateProductParticipatingLoyaltyValidator(IUnitOfWork unitOfWork,
        IStringLocalizer<CommonValidationLocalization> localizer)
    {
        RuleFor(x => x).Must(createProductParticipatingLoyalty =>
            {
                var loyaltyProgram = unitOfWork.Repository<LoyaltyProgram>().Get()
                    .FirstOrDefault(x => x.Id == createProductParticipatingLoyalty.LoyaltyProgramId);
                return loyaltyProgram is not null && (loyaltyProgram.Type == LoyaltyProgramType.QrCode ||
                                                      loyaltyProgram.Type == LoyaltyProgramType.Purchase);
            }).WithName(x => nameof(x.LoyaltyProgramId))
            .WithErrorCode(ErrorCode.IdNotExist).WithMessage(localizer["IdNotExist"]);
        RuleFor(x => x).Must(createProductParticipatingLoyalty =>
        {
            var product = unitOfWork.Repository<Product>().Get().Include(x => x.ParentProduct)
                .FirstOrDefault(x => x.Id == createProductParticipatingLoyalty.ProductId);
            var loyaltyProgram = unitOfWork.Repository<LoyaltyProgram>().Get()
                .FirstOrDefault(x => x.Id == createProductParticipatingLoyalty.LoyaltyProgramId);
            return product is not null && loyaltyProgram is not null &&
                   product.ParentProduct!.VendorId == loyaltyProgram.VendorId;
        }).WithName(x => nameof(x.ProductId)).WithErrorCode(ErrorCode.IdNotExist).WithMessage(localizer["IdNotExist"]);
        RuleFor(x => x.Points).GreaterThan(0).WithErrorCode(ErrorCode.LessThanValue)
            .WithMessage(localizer["MustGreaterThanOneParam", 0]);
        RuleFor(x => x).Must(createProductParticipatingLoyalty =>
            {
                var loyaltyProgram = unitOfWork.Repository<LoyaltyProgram>().Get()
                    .FirstOrDefault(x => x.Id == createProductParticipatingLoyalty.LoyaltyProgramId);
                if (loyaltyProgram is not null && loyaltyProgram.Type == LoyaltyProgramType.Purchase)
                    return createProductParticipatingLoyalty.AmountOfMoney > 0;
                return true;
            }).WithName(x => nameof(x.AmountOfMoney))
            .WithErrorCode(ErrorCode.LessThanValue).WithMessage(localizer["MustGreaterThanOneParam", "0"]);
        RuleFor(x => x).Must(x =>
            {
                var productParticipatingLoyalty = unitOfWork.Repository<ProductParticipatingLoyalty>().Get()
                    .FirstOrDefault(
                        p
                            => p.ProductId == x.ProductId && p.LoyaltyProgramId == x.LoyaltyProgramId);
                return productParticipatingLoyalty == null;
            }).WithName(x => $"{nameof(x.LoyaltyProgramId)}&{nameof(x.ProductId)}").WithErrorCode(ErrorCode.IdExist)
            .WithMessage(localizer["IdExist"]);
    }
}