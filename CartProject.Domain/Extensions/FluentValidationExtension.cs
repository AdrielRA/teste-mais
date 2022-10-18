using CartProject.Domain.Validations;
using FluentValidation;

namespace CartProject.Domain.Extensions;

public static class FluentValidationExtension
{
    public static IRuleBuilderOptions<T, TProperty> WithErrorCode<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule,
        ErrorCode code
    ) => rule.WithErrorCode(code.ToString());
}
