using Application.Abstructions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TKey : IEquatable<TKey>
    where TEntity : EntityBase<TKey>
{
    protected readonly DbContext _context;
    private readonly DbSet<TEntity> _table;

    public Repository(DbContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }

    public IQueryable<TEntity> GetAll()
    {
        return _table;
    }

    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression)
    {
        return _table.Where(expression);
    }

    public async Task<TEntity?> GetAsync(TKey id, CancellationToken ctn = default)
    {
        return await _table.FindAsync([id], cancellationToken: ctn);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken ctn = default)
    {
        return await _table.FirstOrDefaultAsync(expression, ctn);
    }

    public async Task AddAsync(TEntity entity, CancellationToken ctn)
    {
        await _table.AddAsync(entity, ctn);
    }

    public async Task BulkInsertAsync(IEnumerable<TEntity> entities, CancellationToken ctn = default)
    {
        await _table.AddRangeAsync(entities, ctn);
    }

    public void Update(TEntity entity)
    {
        _table.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _table.Remove(entity);
    }

    public async Task<int> BulkDeleteAsync(IEnumerable<TKey> ids, CancellationToken ctn = default)
    {
        return await _table
            .Where(x => ids.Contains(x.Id))
            .ExecuteDeleteAsync(ctn);
    }

    public bool Any(Expression<Func<TEntity, bool>> expression)
    {
        return _table.Any(expression);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken ctn = default)
    {
        return await _table.AnyAsync(expression, ctn);
    }
}
