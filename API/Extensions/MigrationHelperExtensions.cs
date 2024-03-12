using Application.Interfaces;

using Domain.Entities;

using Membership;

using Microsoft.EntityFrameworkCore;

using Persistence.Contexts;

namespace API.Extensions;

public static class MigrationHelperExtensions
{
    public static async Task MigrateAsync(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var tenantDbContext = serviceScope.ServiceProvider.GetRequiredService<ITenantDbContext>();

        Console.WriteLine("Tenant DB migration starts...");
        await tenantDbContext.Database.MigrateAsync();

        if (!await tenantDbContext.Tenants.AnyAsync()) 
        {
            Console.WriteLine("Step-1: Create Tenant...");

            // Step-1: Create Tenant
            var tenant1 = new Tenant()
            {
                OrganizationName = "Vivasoft",
                ConnectionString = "Server=localhost;Database=VS_DB;User Id=sa;Password=sql2019;TrustServerCertificate=true;"
            };

            tenantDbContext.Tenants.Add(tenant1);
            await tenantDbContext.SaveAsync();

            Console.WriteLine("Create user & migrate app db");
            // Step-2: Create Tenant User
            var accountService = serviceScope.ServiceProvider.GetRequiredService<IAccountService>();
            var userId = await accountService.CreateUser("vs@gmail.com", "vs$12345", tenant1.Id);

            // Step-3: Create application User
            var appUser = new ApplicationUser(userId) { };

            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseSqlServer(tenant1.ConnectionString);
            var appDbContext = new ApplicationDbContext(optionBuilder.Options);
            
            await appDbContext.Database.MigrateAsync();

            await appDbContext.Users.AddAsync(appUser);
            await appDbContext.SaveChangesAsync();
            appDbContext.Dispose();

            Console.WriteLine("Seed finished!");
        }
    }
}
