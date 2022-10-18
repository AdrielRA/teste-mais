using CartProject.Domain.Entities;
using System.Linq.Expressions;

namespace CartProject.Domain.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task Delete(Guid id);
    Task Update(TEntity obj);
    Task<TEntity?> Get(Guid id);
    Task<Guid> Insert(TEntity obj);
    Task Delete(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> Get(Expression<Func<TEntity, bool>> predicate);
    Task<TResult?> GetDTO<TResult>(
        Expression<Func<TEntity, TResult>> select,
        Expression<Func<TEntity, bool>> predicate
    );
    Task<IEnumerable<TEntity>> Select(
        Expression<Func<TEntity, bool>> predicate,
        int page = 1,
        int take = 0
    );
    Task<IEnumerable<TResult>> SelectDTO<TResult>(
        Expression<Func<TEntity, TResult>> select,
        Expression<Func<TEntity, bool>> predicate,
        int page = 1,
        int take = 0
    );
}
