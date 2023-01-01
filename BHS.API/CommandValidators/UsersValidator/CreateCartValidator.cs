using BHS.API.Application.Commands.CartCommand;
using BHS.Domain.Entities.Products;
using BHS.Domain.SeedWork;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BHS.API.CommandValidators.UsersValidator;

public class CreateCartValidator : AbstractValidator<CreateCart>
{
    public CreateCartValidator(IUnitOfWork unitOfWork, IStringLocalizer<CommonValidationLocalization> localizer)
    {
        RuleFor(x => x.ProductId)
            .Must(productId =>
                unitOfWork.Repository<Product>().Get().FirstOrDefault(x => x.Id == productId) is not null)
            .WithErrorCode(ErrorCode.IdNotExist).WithMessage(localizer["IdNotExist"]);

        RuleFor(x => x.Quantity).GreaterThan(0).WithErrorCode(ErrorCode.LessThanValue)
            .WithMessage(localizer["MustGreaterThanOneParam", "0"]);
    }
}