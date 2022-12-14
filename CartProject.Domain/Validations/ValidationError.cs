namespace CartProject.Domain.Validations;

public sealed class ValidationError
{
    public string? Code { get; set; }
    public string? Field { get; set; }
    public string? Message { get; set; }
}
