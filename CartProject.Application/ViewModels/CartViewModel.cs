using CartProject.Domain.Interfaces;
using CartProject.Domain.Entities;
using CartProject.Domain.Enums;

namespace CartProject.Application.ViewModels;

public class CartViewModel : IViewModel<Cart, CartViewModel>
{
    public Guid Id { get; set; }
    public decimal Total { get; set; }
    public int ItemsCount { get; set; }
    public CartStatus Status { get; set; }
    public IEnumerable<ItemViewModel> Items { get; set; }

    public CartViewModel() => Items = new List<ItemViewModel>();

    public static CartViewModel FromModel(Cart model) => new()
    {
        Id = model.Id,
        Total = model.Total,
        Status = model.Status,
        ItemsCount = model.Items.Count(),
        Items = model.Items.Select(ItemViewModel.FromModel),
    };
}
