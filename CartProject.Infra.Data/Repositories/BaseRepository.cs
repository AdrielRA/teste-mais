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

    public Task<TEntity?> Get(Guid id) => Get(obj => obj.Id == id);

    public async Task<TEntity?> Get(Expression<Func<TEntity, bool>> predicate) => await _context.Set<TEntity>().FindAsync(predicate);

    public async Task<TResult?> GetDTO<TResult>(
        Expression<Func<TEntity, TResult>> select,
        Expression<Func<TEntity, bool>> predicate
    ) => await _context.Set<TEntity>().Where(predicate).Select(select).FirstOrDefaultAsync();

    public async Task<IEnumerable<TEntity>> Select(
        Expression<Func<TEntity, bool>> predicate,
        int page = 1,
        int take = 0
    ) => await _context.Set<TEntity>().Where(predicate).Skip(take * (page - 1)).Take(take).ToListAsync();

    public async Task<IEnumerable<TResult>> SelectDTO<TResult>(
        Expression<Func<TEntity, TResult>> select,
        Expression<Func<TEntity, bool>> predicate,
        int page = 1,
        int take = 0
    ) => await _context.Set<TEntity>().Where(predicate).Select(select).Skip(take * (page - 1)).Take(take).ToListAsync();
}
