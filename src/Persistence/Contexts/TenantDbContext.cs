using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Domain.Entities;
using System.Reflection;
using Domain.Utilities;

namespace Persistence.Contexts;

public class TenantDbContext : IdentityDbContext<ExtendedIdentityTenantUser, IdentityRole<int>, int>, ITenantDbContext
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
        ConfigureConverterForEncryptedProperty(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    private static void ConfigureConverterForEncryptedProperty(ModelBuilder modelBuilder)
    {
        var encryptedStringConverter = new EncryptedStringConverter();

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var propertiesNeedToEncrypt = entityType.ClrType.GetProperties()
                .Where(p => p.GetCustomAttribute<EncryptedAttribute>() is not null);

            foreach (var property in propertiesNeedToEncrypt)
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property(property.Name)
                    .HasConversion(encryptedStringConverter);
            }
        }
    }
}
