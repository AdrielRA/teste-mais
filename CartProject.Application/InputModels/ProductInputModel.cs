using CartProject.Domain.Interfaces;
using CartProject.Domain.Entities;

namespace CartProject.Application.InputModels;

public class ProductInputModel: IInputModel<Product>
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public decimal Value { get; set; }

    public Product ToModel() => new ()
    {
        Name = Name,
        Code = Code,
        Value = Value
};
}
