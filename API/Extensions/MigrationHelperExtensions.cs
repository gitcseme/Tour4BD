using Application.Interfaces;

using Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class MigrationHelperExtensions
{
    public static async Task MigrateAsync(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var tenantDbContext = serviceScope.ServiceProvider.GetRequiredService<ITenantDbContext>();

        Console.WriteLine("Database migration starts...");
        await tenantDbContext.Database.MigrateAsync();

        if (!await tenantDbContext.Tenants.AnyAsync()) 
        {
            Console.WriteLine("Seeding Tenant db...");

            var tenant1 = new Tenant()
            {
                OrganizationName = "Vivasoft",
                ConnectionString = "Server=localhost;Database=tenantDB_1;User Id=sa;Password=sql2019;TrustServerCertificate=true;"
            };

            tenantDbContext.Tenants.Add(tenant1);
            await tenantDbContext.SaveAsync();

            Console.WriteLine("Seed finished!");
        }
    }
}
