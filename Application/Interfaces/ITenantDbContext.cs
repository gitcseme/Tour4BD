using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Interfaces;

public interface ITenantDbContext 
{
    DbSet<Tenant> Tenants { get; set; }
    DbSet<Permission> Permissions { get; set; }
    DbSet<UserPermission> UserPermissions { get; set; }
    DbSet<ExtendedIdentityUser> Users { get; set; }

    public Task<int> SaveAsync();

    public DatabaseFacade Database { get; }
}
