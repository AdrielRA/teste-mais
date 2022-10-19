namespace CartProject.Domain.Entities;

public class Item: BaseEntity
{
    public Cart? Cart { get; set; }
    public Guid CartId { get; set; }
    public int Quantity { get; set; }
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public decimal SubTotal { get => (Product?.Value ?? 0) * Quantity; }

}
