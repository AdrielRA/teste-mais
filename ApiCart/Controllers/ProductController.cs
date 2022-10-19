using CartProject.Application.Services.Interfaces;
using CartProject.Application.UpdateModels;
using CartProject.Application.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace CartProject.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service) => _service = service;
    
    [HttpGet]
    public async Task<IActionResult> GetAll() => (await _service.GetAll()).GetResponse();

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) => (await _service.GetById(id)).GetResponse();
    
    [HttpPost]
    public async Task<IActionResult> Insert(ProductInputModel input) => (await _service.Insert(input)).GetResponse();

    [HttpPut]
    public async Task<IActionResult> Update(ProductUpdateModel update) => (await _service.Update(update)).GetResponse();

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id) => (await _service.Delete(id)).GetResponse();
}
