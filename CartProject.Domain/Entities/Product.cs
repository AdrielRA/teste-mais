namespace CartProject.Domain.Entities;

public class Product: BaseEntity
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public decimal Value { get; set; }
}
