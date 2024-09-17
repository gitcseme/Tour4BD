using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.UnitOfWorks;

public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    protected readonly TContext _dbContext;
    private Dictionary<Type, object> _typeRepositoryMap;

    public UnitOfWork(TContext dbContext)
    {
        _dbContext = dbContext;
        _typeRepositoryMap = [];
    }

    public async Task<int> SaveAsync() => await _dbContext.SaveChangesAsync();


    public int Save() => _dbContext.SaveChanges();

    public IRepository<TEntity, TKey> Repository<TEntity, TKey>()
        where TEntity : BaseEntity<TKey>
    {
        var type = typeof(TEntity);
        if (!_typeRepositoryMap.ContainsKey(type))
        {
            _typeRepositoryMap[type] = new Repository<TEntity, TKey>(_dbContext);
        }

        return (IRepository<TEntity, TKey>)_typeRepositoryMap[type];
    }
}