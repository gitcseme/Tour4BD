using Application;
using Application.Interfaces;
using Application.Interfaces.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Persistence.Contexts;
using Persistence.Repositories;
using Persistence.UnitOfWorks;

namespace Persistence;

public static class PersistenceDependencyResolverExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        var tenantDbConnectionString = configurationManager.GetConnectionString(AppConstants.TenantDbConnectionStringName)!;
        services.RegisterDatabaseContexts(tenantDbConnectionString);

        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<ITenantUnitOfWork, TenantUnitOfWork>();

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
