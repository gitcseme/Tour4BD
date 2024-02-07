using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Persistence.Contexts;

namespace Persistence;

public static class PersistenceDependencyResolverExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, string tenantDbConnectionString)
    {
        services.AddDbContext<TenantDbContext>(options =>
        {
            options.UseSqlServer(tenantDbConnectionString, builder =>
            {
                builder.CommandTimeout(30);
                builder.EnableRetryOnFailure(3);
            });
        });

        // connection string is dynamic according to Tenant
        services.AddDbContext<ApplicationDbContext>();

        return services;
    }


}
