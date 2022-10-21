using CartProject.Application.UpdateModels;
using CartProject.Domain.Validations;
using CartProject.Domain.Extensions;
using FluentValidation;

namespace CartProject.Application.Validators;

public class ProductUpdateModelValidator : AbstractValidator<ProductUpdateModel>
{
    public ProductUpdateModelValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithErrorCode(ErrorCode.EX00001)
            .NotEmpty()
            .WithErrorCode(ErrorCode.EX00002);
        
        RuleFor(x => x.Name)
            .NotNull()
            .WithErrorCode(ErrorCode.EX00014)
            .NotEmpty()
            .WithErrorCode(ErrorCode.EX00015)
            .Matches(RegexExtension.Name())
            .WithErrorCode(ErrorCode.EX00016)
            .MinimumLength(10)
            .WithErrorCode(ErrorCode.EX00017)
            .MaximumLength(100)
            .WithErrorCode(ErrorCode.EX00018);

        RuleFor(x => x.Code)
            .NotNull()
            .WithErrorCode(ErrorCode.EX00022)
            .NotEmpty()
            .WithErrorCode(ErrorCode.EX00023)
            .Matches(RegexExtension.LettersAdnNumbers())
            .WithErrorCode(ErrorCode.EX00024)
            .MinimumLength(6)
            .WithErrorCode(ErrorCode.EX00025);

        RuleFor(x => x.Value)
            .NotNull()
            .WithErrorCode(ErrorCode.EX00019)
            .NotEmpty()
            .WithErrorCode(ErrorCode.EX00020)
            .GreaterThan(0)
            .WithErrorCode(ErrorCode.EX00021);
    }
}
