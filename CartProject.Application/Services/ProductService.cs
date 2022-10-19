using CartProject.Application.Services.Interfaces;
using CartProject.Application.UpdateModels;
using CartProject.Application.InputModels;
using CartProject.Application.Validators;
using CartProject.Application.ViewModels;
using CartProject.Domain.Validations;
using CartProject.Domain.Interfaces;
using CartProject.Domain.Entities;

namespace CartProject.Application.Services;

public class ProductService : IProductService
{
    private readonly IBaseRepository<Product> _repository;

    public ProductService(IBaseRepository<Product> repository) => _repository = repository;    

    public async Task<ResultResponse<Guid>> Insert(ProductInputModel input)
    {
        var validationResult = new ProductInputModelValidator().Validate(input);
        if (!validationResult.IsValid) return ResultService.RequestError<Guid>("Dados inválidos", validationResult);

        var id = await _repository.Insert(input.ToModel());

        return ResultService.Ok(id);
    }

    public async Task<ResultResponse<ProductViewModel>> GetById(Guid id)
    {
        var product = await _repository.Get(id);
        if (product == null) return ResultService.Fail<ProductViewModel>("Produto não encontrado");

        return ResultService.Ok(ProductViewModel.FromModel(product));
    }

    public async Task<ResultResponse<IList<ProductViewModel>>> GetAll()
    {
        IList<ProductViewModel> products = (await _repository.Select())
            .Select(product => ProductViewModel.FromModel(product))
            .ToList();

        return ResultService.Ok(products);
    }

    public async Task<ResultResponse> Update(ProductUpdateModel update)
    {
        var validationResult = new ProductUpdateModelValidator().Validate(update);
        if (!validationResult.IsValid) return ResultService.RequestError("Dados inválidos", validationResult);

        if (!await _repository.Exists(update.Id)) return ResultService.Fail("Produto não encontrado");

        await _repository.Update(update.ToModel());

        return ResultService.Ok("Produto atualizado com sucesso");
    }

    public async Task<ResultResponse> Delete(Guid id)
    {
        if (!await _repository.Exists(id)) return ResultService.Fail("Produto não encontrado");

        await _repository.Delete(id);

        return ResultService.Ok("Produto deletado com sucesso");
    }
}
