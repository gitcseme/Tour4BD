using Application.Interfaces;

using Persistence.Contexts;

namespace Persistence.UnitOfWorks;

public class ApplicationUnitOfWork : UnitOfWork<TenantDbContext>
{

    public ApplicationUnitOfWork(IApplicationDbContext dbContext)
        : base((dbContext as TenantDbContext)!)
    {
    }

}
