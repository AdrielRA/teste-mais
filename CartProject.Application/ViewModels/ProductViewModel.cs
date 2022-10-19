using CartProject.Domain.Entities;

namespace CartProject.Application.ViewModels;

public class ProductViewModel
{
    public Guid Id { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public decimal Value { get; set; }

    public static ProductViewModel FromModel(Product product) => new ()
    {
        Id = product.Id,
        Code = product.Code,
        Name = product.Name,
        Value = product.Value,
    };
}
