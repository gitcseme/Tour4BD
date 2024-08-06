using Application;
using Application.Interfaces;
using Domain.Entities;
using Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Security.Claims;
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
            var extendedIdentityUser = await CreateDefaultUserAsync(serviceScope, defaultTenant);
            await CreateDefaultPermissionsIfNotExistAsync(tenantDbContext, extendedIdentityUser!);
        }
    }

    private static async Task CreateDefaultPermissionsIfNotExistAsync(ITenantDbContext tenantDbContext, ExtendedIdentityUser extendedIdentityUser)
    {
        var defaultPermissions = new List<Permission>();
        foreach (var permissionName in AppConstants.Permissions.GetDefaultPermissions())
        {
            var permission = new Permission { Name = permissionName };
            defaultPermissions.Add(permission);
        }

        extendedIdentityUser.Permissions = defaultPermissions;

        tenantDbContext.Users.Update(extendedIdentityUser);
        await tenantDbContext.SaveAsync();
    }

    private static async Task<ExtendedIdentityUser?> CreateDefaultUserAsync(IServiceScope serviceScope, Tenant defaultTenant)
    {
        var accountService = serviceScope.ServiceProvider.GetRequiredService<IAccountService>();
        var defaultTenantUser = await accountService.CreateUserAndAssignAdminRoleAsync("vs@gmail.com", "vs$12345", defaultTenant.Id);

        var appUser = new ApplicationUser(defaultTenantUser.Id);
        var appDbContext = GetAppDbContextFromTenantConnectionString(defaultTenant.ConnectionString);
        await appDbContext.Database.MigrateAsync();
        await appDbContext.Users.AddAsync(appUser);
        await appDbContext.SaveAsync();

        return defaultTenantUser;
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
            var newRole = new IdentityRole<int>(roleName);
            await roleManager.CreateAsync(newRole);
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
