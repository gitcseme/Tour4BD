using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    protected readonly TContext _dbContext;
    private Dictionary<Type, object> _typeRepositoryMap;
    private bool disposedValue;

    public UnitOfWork(TContext dbContext)
    {
        _dbContext = dbContext;
        _typeRepositoryMap = [];
    }

    public async Task<int> SaveAsync() => await _dbContext.SaveChangesAsync();


    public int Save() => _dbContext.SaveChanges();

    public IRepository<TEntity, TKey> Repository<TEntity, TKey>()
        where TEntity : EntityBase<TKey>
    {
        var type = typeof(TEntity);
        if (!_typeRepositoryMap.ContainsKey(type))
        {
            _typeRepositoryMap[type] = new Repository<TEntity, TKey>(_dbContext);
        }

        return (IRepository<TEntity, TKey>)_typeRepositoryMap[type];
    }

    public IQueryable<TEntity> GetTable<TEntity>() where TEntity : class
    {
        return _dbContext.Set<TEntity>();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                _typeRepositoryMap?.Clear();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~UnitOfWork()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}