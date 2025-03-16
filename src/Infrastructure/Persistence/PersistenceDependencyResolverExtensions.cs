using Application;
using Application.Abstructions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using SharedKarnel;

namespace Persistence;

public static class PersistenceDependencyResolverExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();

        services.RegisterDatabaseContexts(configurationManager);

        return services;
    }


    private static IServiceCollection RegisterDatabaseContexts(this IServiceCollection services, IConfigurationManager configManager)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configManager.GetConnectionString(AppConstants.MsSqlConnection), builder =>
            {
                builder.CommandTimeout(30);
                builder.EnableRetryOnFailure(3);
            })
                .EnableDetailedErrors(true)
                .EnableSensitiveDataLogging(true);
        });

        return services;
    }

}
