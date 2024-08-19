using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories;

public interface IRepository<TEntity, TKey>
    where TEntity : class
{
    IQueryable<TEntity> GetAll();
    Task<TEntity?> GetAsync(TKey id);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression);
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression);
}
