using Application.Interfaces;
using Application.Interfaces.Repositories;

using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence.UnitOfWorks;

public class TenantUnitOfWork : UnitOfWork<TenantDbContext>
{

    public TenantUnitOfWork(ITenantDbContext dbContext)
        : base((dbContext as TenantDbContext)!)
    {
    }

}
