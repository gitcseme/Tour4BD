using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories;

public interface IRepository<TEntity, TKey>
{
    Task<TEntity?> GetAsync(TKey id);
    IQueryable<TEntity> GetAll();
}
