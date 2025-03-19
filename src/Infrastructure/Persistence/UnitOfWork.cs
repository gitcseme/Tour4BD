using Application.Abstructions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence;

public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    protected readonly TContext _context;
    private readonly Dictionary<Type, object> _typeRepositoryDict;

    public UnitOfWork(TContext context)
    {
        _context = context;
        _typeRepositoryDict = [];
    }

    public int Save()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveAsync(CancellationToken ctn)
    {
        return await _context.SaveChangesAsync(ctn);
    }

    public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
        where TKey : IEquatable<TKey>
        where TEntity : EntityBase<TKey>
    {
        var type = typeof(TEntity);
        if (_typeRepositoryDict.TryGetValue(type, out var resolvedRepository))
        {
            return (IRepository<TEntity, TKey>) resolvedRepository;
        }

        var repository = new Repository<TEntity, TKey>(_context);
        _typeRepositoryDict.Add(type, repository);

        return repository;
    }

    public DbContext GetUnderlyingDbContext() => _context;
    

    public IQueryable<TEntity> GetTable<TEntity>()
        where TEntity : class
    {
        return _context.Set<TEntity>();
    }

    public DbSet<TEntity> GetDbSet<TEntity>()
        where TEntity : class
    {
        return _context.Set<TEntity>();
    }

}