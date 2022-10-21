using CartProject.Application.Services.Interfaces;
using CartProject.Application.ViewModels;
using CartProject.Domain.Validations;
using CartProject.Domain.Interfaces;
using CartProject.Domain.Entities;

namespace CartProject.Application.Services;

public class CartService: ICartService
{
    private readonly IBaseRepository<Cart> _repository;
    private readonly IBaseRepository<Product> _productRepository;

    public CartService(IBaseRepository<Cart> repository, IBaseRepository<Product> productRepository)
    {
        _repository = repository;
        _productRepository = productRepository;
    }

    public async Task<ResultResponse> AddItem(Guid productId, Guid cartId, int quantity)
    {
        await _repository.BeginTransaction();

        if (cartId == Guid.Empty) return ResultService.Fail("Id do carrinho deve ser informado");
        if (productId == Guid.Empty) return ResultService.Fail("Id do produto deve ser informado");
        if (quantity <= 0) return ResultService.Fail("Quantidade informada é inválida");

        if (!await _productRepository.Exists(productId)) return ResultService.Fail("Produto não encontrado");

        Cart cart = new()
        {
            Id = cartId,
            Status = Domain.Enums.CartStatus.OPENED,
            Items = new List<Item>()
        };

        if (!await _repository.Exists(cartId)) await _repository.Insert(cart);
        else cart = await _repository.Get(cartId, includes: new() { c => c.Items }, hasTracking: true) ?? cart;

        if (cart.Status != Domain.Enums.CartStatus.OPENED) return ResultService.Fail("Não é possível adicionar um produto a um carrinho finalizado");

        Item? item = cart.Items.FirstOrDefault(item => item.ProductId == productId);

        if (item == null)
        {
            cart.Items.Add(new Item()
            {
                ProductId = productId,
                CartId = cartId,
                Quantity = quantity
            });
        }
        else item.Quantity += quantity;

        await _repository.Update(cart);

        await _repository.CommitTransaction();

        return ResultService.Ok("Produto adicionado ao carrinho");
    }

    public async Task<ResultResponse<CartViewModel>> CheckOut(Guid cartId)
    {
        if (cartId == Guid.Empty) return ResultService.Fail<CartViewModel>("Id do carrinho deve ser informado");
        
        Cart? cart = await _repository.Get(cartId, "Items.Product");

        if(cart == null) return ResultService.Fail<CartViewModel>("Carrinho não encontrado");
        else
        {
            if(cart.Status == Domain.Enums.CartStatus.CLOSED) return ResultService.Fail<CartViewModel>("Carrinho já foi finalizado");
            if (cart.Items.Count == 0) return ResultService.Fail<CartViewModel>("Não é possível fechar um carrinho vazio");

            cart.Status = Domain.Enums.CartStatus.CLOSED;

            await _repository.Update(cart);

            return ResultService.Ok(CartViewModel.FromModel(cart));
        }
    }

    public async Task<ResultResponse> Delete(Guid cartId)
    {
        if (cartId == Guid.Empty) return ResultService.Fail<CartViewModel>("Id do carrinho deve ser informado");

        Cart? cart = await _repository.Get(cartId, includes: new() { c => c.Items }, hasTracking: true);
        if (cart == null) return ResultService.Fail("Carrinho não encontrado");

        cart.Items.Clear();
        await _repository.Delete(cartId);

        return ResultService.Ok("Carrinho foi deletado");
    }

    public async Task<ResultResponse<IEnumerable<CartViewModel>>> GetAll() => 
        ResultService.Ok((await _repository.Select(include: "Items.Product")).Select(CartViewModel.FromModel));
    
    public async Task<ResultResponse<IEnumerable<ItemViewModel>>> GetItems(Guid cartId)
    {
        if (cartId == Guid.Empty) return ResultService.Fail<IEnumerable<ItemViewModel>>("Id do carrinho deve ser informado");
        if (!await _repository.Exists(cartId)) return ResultService.Fail<IEnumerable<ItemViewModel>>("Carrinho não encontrado");

        var items = (await _repository.Get(cartId, "Items.Product"))?.Items.Select(ItemViewModel.FromModel) ?? new List<ItemViewModel>();

        return ResultService.Ok(items);
    }

    public async Task<ResultResponse> RemoveItem(Guid itemId, Guid cartId, bool? removeAll = false)
    {
        if (cartId == Guid.Empty) return ResultService.Fail("Id do carrinho deve ser informado");

        Cart? cart = await _repository.Get(cartId, includes: new() { c => c.Items }, hasTracking: true);
        if (cart == null) return ResultService.Fail("Carrinho não encontrado");
        else
        {
            if (cart.Status == Domain.Enums.CartStatus.CLOSED)
                return ResultService.Fail("Não é possível retirar item de um carrinho que já foi finalizado");

            Item? item = cart.Items.FirstOrDefault(item => item.Id == itemId && item.Quantity > 0);

            if(item == null) return ResultService.Fail("Item não foi encontrado no carrinho");
            else
            {
                if (item.Quantity == 1 || removeAll == true) cart.Items.Remove(item);
                else item.Quantity -= 1;

                await _repository.Update(cart);
            }
        }

        return ResultService.Ok("Item removido do carrinho");
    }
}
