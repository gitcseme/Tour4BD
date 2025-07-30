using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKarnel;

namespace Application;

public static class ApplicationDependencyResolverEstensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfigurationManager configuration)
    {
        services
            .ConfigureMadiatR()
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddRedisConfiguration(configuration);

        return services;
    }

    private static IServiceCollection AddRedisConfiguration(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString(AppConstants.RedisConnection);
        });
        return services;
    }

    private static IServiceCollection ConfigureMadiatR(this IServiceCollection services)
    {
        return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}
