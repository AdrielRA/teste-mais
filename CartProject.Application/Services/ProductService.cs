using CartProject.Application.InputModels.Validators;
using CartProject.Application.Services.Interfaces;
using CartProject.Application.InputModels;
using CartProject.Application.ViewModels;
using CartProject.Domain.Validations;
using CartProject.Domain.Interfaces;
using CartProject.Domain.Entities;

namespace CartProject.Application.Services;

public class ProductService : IProductService
{
    private readonly IBaseRepository<Product> _repository;

    public ProductService(IBaseRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<ResultResponse<Guid>> Insert(ProductInputModel input)
    {
        var validationResult = new ProductInputModelValidator().Validate(input);
        if (!validationResult.IsValid) return ResultService.RequestError<Guid>("Dados inválidos", validationResult);
        var id = await _repository.Insert(input.ToModel());
        return ResultService.Ok(id);
    }

    public async Task<ResultResponse<ProductViewModel>> GetById(Guid id) {
        var product = await _repository.Get(id);
        if (product == null) return ResultService.Fail<ProductViewModel>("Produto não encontrado");
        return ResultService.Ok(ProductViewModel.FromModel(product));
    }
}
