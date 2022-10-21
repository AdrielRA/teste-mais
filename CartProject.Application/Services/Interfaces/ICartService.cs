using CartProject.Application.ViewModels;
using CartProject.Domain.Validations;

namespace CartProject.Application.Services.Interfaces;

public interface ICartService
{
    Task<ResultResponse> Delete(Guid cartId);
    Task<ResultResponse<IEnumerable<CartViewModel>>> GetAll();
    Task<ResultResponse<CartViewModel>> CheckOut(Guid cartId);
    Task<ResultResponse<IEnumerable<ItemViewModel>>> GetItems(Guid cartId);
    Task<ResultResponse> AddItem(Guid productId, Guid cartId, int quantity);
    Task<ResultResponse> RemoveItem(Guid itemId, Guid cartId, bool? removeAll = false);
}
