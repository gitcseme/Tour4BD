using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories;

public interface IRepository<TEntity, TKey>
    where TEntity : class
{
    Task<TEntity?> GetAsync(TKey id);
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression);
}
