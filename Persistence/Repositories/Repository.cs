using Application.Interfaces.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public abstract class Repository<TContext, TEntity, TKey> : IRepository<TEntity, TKey>
    where TContext : DbContext
    where TEntity : class
{
    private readonly DbSet<TEntity> _table;

    public Repository(TContext context)
    {
        _table = context.Set<TEntity>();
    }

    public IQueryable<TEntity> GetAll() =>  _table;

    public async Task<TEntity?> GetAsync(TKey id) => await _table.FindAsync(id);
}
