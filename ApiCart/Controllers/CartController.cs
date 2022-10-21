using CartProject.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CartProject.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _service;

    public CartController(ICartService service) => _service = service;
    
    [HttpGet]
    public async Task<IActionResult> GetAll() => (await _service.GetAll()).GetResponse();
    
    [HttpGet("{cartId}/items")]
    public async Task<IActionResult> GetItems(Guid cartId) => (await _service.GetItems(cartId)).GetResponse();
    
    [HttpPost("{cartId}/add-item")]
    public async Task<IActionResult> AddItem(Guid cartId, [FromQuery] Guid productId, [FromQuery] int quantity) => 
        (await _service.AddItem(productId, cartId, quantity)).GetResponse();
    
    [HttpPut("check-out")]
    public async Task<IActionResult> CheckOut([FromQuery] Guid cartId) => (await _service.CheckOut(cartId)).GetResponse();
    
    [HttpDelete("{cartId}/remove-item")]
    public async Task<IActionResult> RemoveItem(Guid cartId, [FromQuery] Guid itemId, [FromQuery] bool removeAll) =>
        (await _service.RemoveItem(itemId, cartId, removeAll)).GetResponse();

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] Guid cartId) => (await _service.Delete(cartId)).GetResponse();

}
