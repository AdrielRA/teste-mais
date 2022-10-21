namespace CartProject.Domain.Interfaces;

using CartProject.Domain.Entities;

public interface IInputModel<T> where T : BaseEntity
{
    T ToModel();
}
