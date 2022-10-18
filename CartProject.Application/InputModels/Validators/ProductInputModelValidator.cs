using CartProject.Domain.Validations;
using CartProject.Domain.Extensions;
using FluentValidation;

namespace CartProject.Application.InputModels.Validators;

public class ProductInputModelValidator: AbstractValidator<ProductInputModel>
{
    public ProductInputModelValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .WithErrorCode(ErrorCode.EX00001)
            .NotEmpty()
            .WithErrorCode(ErrorCode.EX00002)
            .MinimumLength(10)
            .MaximumLength(100);
            
    }
}

