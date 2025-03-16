using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Abstructions;

public interface IUnitOfWork
{
    IRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
        where TEntity : EntityBase<TKey>
        where TKey : IEquatable<TKey>;

    int Save();

    Task<int> SaveAsync(CancellationToken ctn);

    DbContext GetUnderlyingDbContext();

    IQueryable<TEntity> GetTable<TEntity>() where TEntity : class;

    DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;

}