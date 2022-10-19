using CartProject.Domain.Entities;

namespace CartProject.Application.UpdateModels;

public class ProductUpdateModel
{
    public Guid Id { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public decimal Value { get; set; }

    public Product ToModel() => new()
    {
        Id = Id,
        Code = Code,
        Name = Name,
        Value = Value
    };
}
