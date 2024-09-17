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

        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        //services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork<>)); // fails because no resolve possible at runtime. no other type providing it type info.

        services.AddKeyedScoped<IUnitOfWork, UnitOfWork<TenantDbContext>>(AppConstants.TenantDbContextDIKey);
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>(AppConstants.ApplicationDbContextDIKey);

        //services.AddKeyedScoped<IUnitOfWork, TenantUnitOfWork>(AppConstants.TenantDbContextDIKey);
        //services.AddKeyedScoped<IUnitOfWork, ApplicationUnitOfWork>(AppConstants.ApplicationDbContextDIKey);



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
