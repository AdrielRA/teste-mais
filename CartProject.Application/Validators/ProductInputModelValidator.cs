using CartProject.Application.InputModels;
using CartProject.Domain.Validations;
using CartProject.Domain.Extensions;
using FluentValidation;

namespace CartProject.Application.Validators;

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

        RuleFor(x => x.Value)
            .NotNull()
            .WithErrorCode(ErrorCode.EX00001)
            .NotEmpty()
            .WithErrorCode(ErrorCode.EX00002)
            .GreaterThan(0)
            .WithErrorCode(ErrorCode.EX00002);
    }
}

