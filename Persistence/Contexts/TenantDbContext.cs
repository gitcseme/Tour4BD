using Domain.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Persistence.Contexts;

public class TenantDbContext : IdentityDbContext<ExtendedIdentityUser, IdentityRole<int>, int>, ITenantDbContext
{
    public TenantDbContext(DbContextOptions<TenantDbContext> options) 
        : base(options)
    {
    }

    public DbSet<Tenant> Tenants { get; set; }

    public async Task<int> SaveAsync()
    {
        return await base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tenant>().HasKey(t => t.Id);
        modelBuilder.Entity<Tenant>().Property(t => t.ConnectionString).IsRequired();
        modelBuilder.Entity<Tenant>().Property(t => t.OrganizationName).IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}
