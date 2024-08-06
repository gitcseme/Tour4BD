using Application.Interfaces.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class TenantRepository : Repository<TenantDbContext, Tenant, int>, ITenantRepository
{
    public TenantRepository(TenantDbContext context) : base(context)
    {
    }
}