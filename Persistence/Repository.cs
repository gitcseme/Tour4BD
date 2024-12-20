﻿using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class
{
    protected readonly DbContext _context;
    private readonly DbSet<TEntity> _table;

    public Repository(DbContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }

    public IQueryable<TEntity> Query(bool asNoTracking = true) => asNoTracking ? _table.AsNoTracking() : _table;

    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression) => _table.Where(expression);

    public async Task<TEntity?> GetAsync(TKey id) => await _table.FindAsync(id);

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression) => await _table.FirstOrDefaultAsync(expression);

    public async Task AddAsync(TEntity entity, CancellationToken ct = default)
    {
        await _table.AddAsync(entity, ct);
    }
}
