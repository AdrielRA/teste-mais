using CartProject.Domain.Entities;

namespace CartProject.Application.ViewModels;

public class ProductViewModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public static ProductViewModel FromModel(Product product) => new ()
    {
        Id = product.Id,
        Name = product.Name,
    };
}
