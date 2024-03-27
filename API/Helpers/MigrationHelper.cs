using Application;
using Application.Interfaces;
using Domain.Entities;
using Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using System.Text.RegularExpressions;

namespace API.Helpers;

public class MigrationHelper(WebApplication application)
{
    public async Task SeedAsync()
    {
        using var serviceScope = application.Services.CreateScope();

        var tenantDbContext = serviceScope.ServiceProvider.GetRequiredService<ITenantDbContext>();
        await tenantDbContext.Database.MigrateAsync();
        
        await CreateRolesIfNotExistAsync(serviceScope);

        if (!await tenantDbContext.Tenants.AnyAsync())
        {
            var defaultTenant = await CreateDefaultTenantAsync(tenantDbContext);
            await CreateDefaultUserAsync(serviceScope, defaultTenant);
        }
    }


    private async Task CreateDefaultUserAsync(IServiceScope serviceScope, Tenant defaultTenant)
    {
        var accountService = serviceScope.ServiceProvider.GetRequiredService<IAccountService>();
        var defaultTenantUser = await accountService.CreateUserAndAssignAdminRoleAsync("vs@gmail.com", "vs$12345", defaultTenant.Id);

        var appUser = new ApplicationUser(defaultTenantUser.Id);
        var appDbContext = GetAppDbContextFromTenantConnectionString(defaultTenant.ConnectionString);
        await appDbContext.Database.MigrateAsync();
        await appDbContext.Users.AddAsync(appUser);
        await appDbContext.SaveAsync();
    }

    private static async Task<Tenant> CreateDefaultTenantAsync(ITenantDbContext tenantDbContext)
    {
        string tenantConnectionString = tenantDbContext.Database.GetConnectionString()!;
        string userConnectionString = new Regex("Database=(\\w+)").Replace(tenantConnectionString, "Database=VS_DB");

        var defaultTenant = new Tenant
        {
            ConnectionString = userConnectionString,
            OrganizationName = "Vivasoft",
        };

        await tenantDbContext.Tenants.AddAsync(defaultTenant);
        await tenantDbContext.SaveAsync();
        return defaultTenant;
    }

    private static async Task CreateRolesIfNotExistAsync(IServiceScope serviceScope)
    {
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

        if (await roleManager.Roles.AnyAsync())
        {
            return;
        }

        foreach (var roleName in AppConstants.Roles.GetAll())
        {
            await roleManager.CreateAsync(new IdentityRole<int>(roleName));
        }
    }
    
    private static IApplicationDbContext GetAppDbContextFromTenantConnectionString(string connectionString)
    {
        return new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options
        );
    }
}
