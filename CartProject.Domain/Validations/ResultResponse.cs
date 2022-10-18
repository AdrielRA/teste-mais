using Microsoft.AspNetCore.Mvc;

namespace CartProject.Domain.Validations;

public class ResultResponse
{
    public string? Message { get; set; }
    public string? ErrorCode { get; set; }
    public ICollection<ValidationError>? Errors { get; set; }
    public bool Success { get => string.IsNullOrEmpty(ErrorCode) && (!Errors?.Any() ?? true); }

    public IActionResult GetResponse() => Success ? new OkObjectResult(this) : new BadRequestObjectResult(this);
}

public class ResultResponse<T> : ResultResponse
{
    public T? Data { get; set; }
}
