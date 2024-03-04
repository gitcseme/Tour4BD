using Application.Interfaces;
using Application.Interfaces.Repositories;

using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence.UnitOfWorks;

public class TenantUnitOfWork : UnitOfWorkBase<TenantDbContext>, ITenantUnitOfWork
{
    private readonly ITenantDbContext _dbContext;
    private ITenantRepository? _tenantRepository;

    public TenantUnitOfWork(ITenantDbContext dbContext) 
        : base((dbContext as TenantDbContext)!)
    {
        _dbContext = dbContext;
    }

    public ITenantRepository TenantRepository => _tenantRepository is null 
        ? (_tenantRepository = new TenantRepository((_dbContext as TenantDbContext)!)) 
        : _tenantRepository;

}
