using System.Reflection;

using Application.Interfaces;
using Application.Mapping;

using AutoMapper;

using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationDependencyResolverEstensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.ConfigureMadiatR();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }

    private static IServiceCollection ConfigureMadiatR(this IServiceCollection services)
    {
        return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}
