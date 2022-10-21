namespace CartProject.Domain.Interfaces;
using CartProject.Domain.Entities;

public interface IViewModel<T, TResult> where T : BaseEntity
{
    #pragma warning disable CA2252 // Esta API requer a aceitação de recursos de visualização
    static abstract TResult FromModel(T model);
    #pragma warning restore CA2252 // Esta API requer a aceitação de recursos de visualização
}
