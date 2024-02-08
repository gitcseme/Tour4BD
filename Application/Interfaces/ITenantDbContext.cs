using System.Threading.Tasks;

using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Interfaces;

public interface ITenantDbContext 
{
    DbSet<Tenant> Tenants { get; set; }

    public Task<int> SaveAsync();

    public DatabaseFacade Database { get; }
}
