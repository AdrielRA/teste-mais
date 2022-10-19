using CartProject.Domain.Enums;

namespace CartProject.Domain.Entities;

public class Cart: BaseEntity
{
    public CartStatus Status { get; set; }
    public IEnumerable<Item> Items { get; set; }

    public decimal Total { get => Items.Sum(i => i.SubTotal); }

    public Cart()
    {
        Items = new List<Item>();
    }
}
