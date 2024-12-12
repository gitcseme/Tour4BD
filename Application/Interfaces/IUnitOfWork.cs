using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IQueryable<TEntity> GetTable<TEntity>() where TEntity : class;
    IRepository<TEntity, TKey> Repository<TEntity, TKey>()  where TEntity : EntityBase<TKey>;
    int Save();
    Task<int> SaveAsync();
}
