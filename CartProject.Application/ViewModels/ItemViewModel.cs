using CartProject.Domain.Interfaces;
using CartProject.Domain.Entities;

namespace CartProject.Application.ViewModels;

public class ItemViewModel : IViewModel<Item, ItemViewModel>
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal SubTotal { get; set; }
    public ProductViewModel? Product { get; set; }

    public static ItemViewModel FromModel(Item model) => new()
    {
        Id = model.Id,
        Quantity = model.Quantity,
        SubTotal = model.SubTotal,
        Product = model.Product != null ? ProductViewModel.FromModel(model.Product) : null
    };
}
