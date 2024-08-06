using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Persistence.EntityConfigurations;
using Domain.Entities;
using System.Reflection;

namespace Persistence.Contexts;

public class TenantDbContext : IdentityDbContext<ExtendedIdentityUser, IdentityRole<int>, int>, ITenantDbContext
{
    public TenantDbContext(DbContextOptions<TenantDbContext> options) 
        : base(options)
    {
    }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }

    public async Task<int> SaveAsync()
    {
        return await base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new TenantConfiguration());
        //modelBuilder.ApplyConfiguration(new EntendedIdentityUserConfiguration());
        //modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        //modelBuilder.ApplyConfiguration(new UserPermissionConfiguration());

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
