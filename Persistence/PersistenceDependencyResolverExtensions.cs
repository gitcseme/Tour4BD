using System.Reflection;

using Application.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Persistence.Contexts;

namespace Persistence;

public static class PersistenceDependencyResolverExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, string tenantDbConnectionString)
    {
        services.RegisterDatabaseContexts(tenantDbConnectionString);

        return services;
    }


    private static IServiceCollection RegisterDatabaseContexts(this IServiceCollection services, string tenantDbConnectionString)
    {
        services.AddDbContext<TenantDbContext>(options =>
        {
            options.UseSqlServer(tenantDbConnectionString, builder =>
            {
                builder.CommandTimeout(30);
                builder.EnableRetryOnFailure(3);
            })
                .EnableDetailedErrors(true)
                .EnableSensitiveDataLogging(true);
        });
        //services.AddScoped<ITenantDbContext>(sp => sp.GetRequiredService<TenantDbContext>());
        services.AddScoped<ITenantDbContext, TenantDbContext>();


        // connection string is dynamic according to Tenant
        services.AddDbContext<ApplicationDbContext>();
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();


        return services;
    }

}
