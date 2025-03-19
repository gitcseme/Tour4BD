using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Abstructions;

public interface IRepository<TEntity, TKey> where TEntity : EntityBase<TKey>
{
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken ctn = default);
    Task<TEntity?> GetAsync(TKey id, CancellationToken ctn = default);

    Task AddAsync(TEntity entity, CancellationToken ctn = default);
    Task BulkInsertAsync(IEnumerable<TEntity> entities, CancellationToken ctn = default);

    void Update(TEntity entity);

    // Delete methods
    void Delete(TEntity entity);
    Task<int> BulkDeleteAsync(IEnumerable<TKey> ids, CancellationToken ctn = default);
    bool Any(Expression<Func<TEntity, bool>> expression);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken ctn = default);
}
