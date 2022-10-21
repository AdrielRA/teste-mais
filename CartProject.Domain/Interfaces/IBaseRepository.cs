using CartProject.Domain.Entities;
using System.Linq.Expressions;

namespace CartProject.Domain.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task RollbackTransaction();
    Task CommitTransaction();
    Task BeginTransaction();

    Task Delete(Guid id);
    Task Update(TEntity obj);
    Task<bool> Exists(Guid id);
    Task<Guid> Insert(TEntity obj);
    Task Delete(Expression<Func<TEntity, bool>> predicate);
    Task<bool> Exists(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> Get(
        Guid id,
        string? include = null,
        List<Expression<Func<TEntity, dynamic>>>? includes = null,
        bool hasTracking = false
    );
    Task<TEntity?> Get(
        Expression<Func<TEntity, bool>> predicate,
        string? include = null,
        List<Expression<Func<TEntity, dynamic>>>? includes = null,
        bool hasTracking = false
    );
    Task<TResult?> GetDTO<TResult>(
        Expression<Func<TEntity, TResult>> select,
        Expression<Func<TEntity, bool>> predicate,
        string? include,
        List<Expression<Func<TEntity, dynamic>>>? includes = null,
        bool hasTracking = false
    );
    Task<IEnumerable<TEntity>> Select(
        Expression<Func<TEntity, bool>>? predicate = null,
        string? include = null,
        List<Expression<Func<TEntity, dynamic>>>? includes = null,
        bool hasTracking = false,
        int page = 1,
        int take = 0
    );
    Task<IEnumerable<TResult>> SelectDTO<TResult>(
        Expression<Func<TEntity, TResult>> select,
        Expression<Func<TEntity, bool>>? predicate = null,
        string? include = null,
        List<Expression<Func<TEntity, dynamic>>>? includes = null,
        bool hasTracking = false,
        int page = 1,
        int take = 0
    );
}
