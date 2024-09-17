using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IUnitOfWork
{
    IRepository<TEntity, TKey> Repository<TEntity, TKey>()  where TEntity : BaseEntity<TKey>;
    int Save();
    Task<int> SaveAsync();
}
