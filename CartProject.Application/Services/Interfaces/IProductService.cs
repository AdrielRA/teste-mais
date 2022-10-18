using CartProject.Application.InputModels;
using CartProject.Application.ViewModels;
using CartProject.Domain.Validations;

namespace CartProject.Application.Services.Interfaces;

public interface IProductService
{
    Task<ResultResponse<Guid>> Insert(ProductInputModel input);
    Task<ResultResponse<ProductViewModel>> GetById(Guid id);
}
