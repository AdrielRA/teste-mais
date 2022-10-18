using CartProject.Domain.Validations;
using CartProject.Domain.Extensions;
using FluentValidation.Results;

namespace CartProject.Application.Services;

public static class ResultService
{
    private static ICollection<ValidationError>? FormatErrors(List<ValidationFailure> errors) =>
        errors?.Select(e =>
        {
            ErrorCode? code = null;
            try { code = e.ErrorCode.ToEnum<ErrorCode>(); } catch { }
            return new ValidationError()
            {
                Field = e.PropertyName,
                Code = code?.ToString() ?? e.ErrorCode,
                Message = code?.GetDescription() ?? e.ErrorMessage,
            };
        }).ToList();

    public static ResultResponse RequestError(string message, ValidationResult validationResult) => new()
    {
        Message = message,
        Errors = FormatErrors(validationResult.Errors)
    };

    public static ResultResponse<T> RequestError<T>(string message, ValidationResult validationResult) => new()
    {
        Message = message,
        Errors = FormatErrors(validationResult.Errors)
    };

    public static ResultResponse<T> Fail<T>(ErrorCode? code) => new() { Message = code.GetDescription(), ErrorCode = code.ToString() };
    public static ResultResponse<T> Fail<T>(string message) => new() { Message = message, ErrorCode = ErrorCode.EX00000.ToString() };
    public static ResultResponse Fail(ErrorCode? code) => new() { Message = code.GetDescription(), ErrorCode = code.ToString() };
    public static ResultResponse Fail(string message) => new() { Message = message, ErrorCode = ErrorCode.EX00000.ToString() };
    public static ResultResponse Ok(string message) => new() { Message = message };
    public static ResultResponse<T> Ok<T>(T data) => new() { Data = data };
}

