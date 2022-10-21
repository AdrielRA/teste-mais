using CartProject.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using CartProject.Domain.Interfaces;
using CartProject.Domain.Entities;
using System.Linq.Expressions;

namespace CartProject.Infra.Data.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity, new()
{

    protected readonly InMemoryContext _context;
    public BaseRepository(InMemoryContext context) { _context = context; }

    public Task RollbackTransaction()
    {
        return Task.CompletedTask;
        //await _context.Database.RollbackTransactionAsync();
    }
    public Task CommitTransaction()
    {
        return Task.CompletedTask;
        //await _context.Database.CommitTransactionAsync();
    }
    public Task BeginTransaction()
    {
        return Task.CompletedTask;
        //await _context.Database.BeginTransactionAsync();
    }

    public async Task<Guid> Insert(TEntity obj)
    {
        _context.Entry(obj).State = EntityState.Added;
        await _context.SaveChangesAsync();
        return obj.Id;
    }

    public async Task Update(TEntity obj)
    {
        _context.Entry(obj).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        TEntity obj = new() { Id = id };
        _context.Entry(obj).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Expression<Func<TEntity, bool>> predicate)
    {
        Guid objId = await GetDTO(obj => obj.Id, predicate);
        await Delete(objId);
    }

    public async Task<bool> Exists(Guid id) => await Exists(obj => obj.Id == id);
    public async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate) =>
        await _context.Set<TEntity>().AnyAsync(predicate);

    public Task<TEntity?> Get(
        Guid id,
        string? include = null,
        List<Expression<Func<TEntity, dynamic>>>? includes = null,
        bool hasTracking = false
    ) => Get(obj => obj.Id == id, include, includes, hasTracking);

    public async Task<TEntity?> Get(
        Expression<Func<TEntity, bool>> predicate,
        string? include = null,
        List<Expression<Func<TEntity, dynamic>>>? includes = null,
        bool hasTracking = false
    ) => await _context
        .Set<TEntity>()
        .WithTracking(hasTracking)
        .WithStringInclude(include)
        .WithIncludes(includes)
        .FirstOrDefaultAsync(predicate);
    
    public async Task<TResult?> GetDTO<TResult>(
        Expression<Func<TEntity, TResult>> select,
        Expression<Func<TEntity, bool>> predicate,
        string? include = null,
        List<Expression<Func<TEntity, dynamic>>>? includes = null,
        bool hasTracking = false
    ) => await _context.Set<TEntity>()
        .WithTracking(hasTracking)
        .WithStringInclude(include)
        .WithIncludes(includes)
        .Where(predicate)
        .Select(select)
        .FirstOrDefaultAsync();

    public async Task<IEnumerable<TEntity>> Select(
        Expression<Func<TEntity, bool>>? predicate = null,
        string? include = null,
        List<Expression<Func<TEntity, dynamic>>>? includes = null,
        bool hasTracking = false,
        int page = 1,
        int take = 0
    ) => await _context.Set<TEntity>()
        .WithTracking(hasTracking)
        .WithStringInclude(include)
        .WithIncludes(includes)
        .Where(predicate ?? (obj => true))
        .Skip(take * (page - 1))
        .Take(take == 0 ? int.MaxValue : take)
        .ToListAsync();

    public async Task<IEnumerable<TResult>> SelectDTO<TResult>(
        Expression<Func<TEntity, TResult>> select,
        Expression<Func<TEntity, bool>>? predicate = null,
        string? include = null,
        List<Expression<Func<TEntity, dynamic>>>? includes = null,
        bool hasTracking = false,
        int page = 1,
        int take = 0
    ) => await _context.Set<TEntity>()
        .WithTracking(hasTracking)
        .WithStringInclude(include)
        .WithIncludes(includes)
        .Where(predicate ?? (obj => true))
        .Select(select)
        .Skip(take * (page - 1))
        .Take(take == 0 ? int.MaxValue : take)
        .ToListAsync();
}

public static class BaseRepositoryExtension
{
    public static IQueryable<TEntity> WithTracking<TEntity>(
        this IQueryable<TEntity> query,
        bool hasTracking
    ) where TEntity : BaseEntity => hasTracking ? query : query.AsNoTracking();

    public static IQueryable<TEntity> WithStringInclude<TEntity>(
        this IQueryable<TEntity> query,
        string? include = null
    ) where TEntity : BaseEntity
    {
        if(!string.IsNullOrEmpty(include))
        {
            foreach (var includeProperty in include.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }
        }

        return query;
    }

    public static IQueryable<TEntity> WithIncludes<TEntity>(
        this IQueryable<TEntity> query,
        IList<Expression<Func<TEntity, dynamic>>>? includes
    ) where TEntity : BaseEntity
    {
        if (includes != null)
        {
            foreach (Expression<Func<TEntity, dynamic>>? include in includes)
            {
                query = query.Include(include);
            }
        }     

        return query;
    }
}
