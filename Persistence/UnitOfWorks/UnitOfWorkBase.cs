using Microsoft.EntityFrameworkCore;

namespace Persistence.UnitOfWorks;

public abstract class UnitOfWorkBase<TContext>
    where TContext : DbContext
{
    private readonly TContext _dbContext;
    public UnitOfWorkBase(TContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveAsync() => await _dbContext.SaveChangesAsync();
    

    public int Save() => _dbContext.SaveChanges();
}