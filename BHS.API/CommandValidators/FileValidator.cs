using FluentValidation;

namespace BHS.API.CommandValidators;

public class FileValidator : AbstractValidator<IFormFile>
{
    public FileValidator()
    {
        //5,000,000 = 5MB
        RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(5_000_000)
            .WithMessage("File size is larger than allowed, size <= 5MB");

        RuleFor(x => x.ContentType).NotNull()
            .Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
            .WithMessage("File type is not allowed, allowed : image/jpeg, image/jpg, image/png");
    }
}