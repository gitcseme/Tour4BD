using Microsoft.EntityFrameworkCore;

using Persistence.Contexts;

namespace API.MigrationHelpers
{
    public static class MigrationHelperExtensions
    {
        public static async Task MigrateAsync(this WebApplication app)
        {
            using var serviceScope = app.Services.CreateScope();
            var tenantDbContext = serviceScope.ServiceProvider.GetRequiredService<TenantDbContext>();
            await tenantDbContext.Database.MigrateAsync();
        }
    }
}
