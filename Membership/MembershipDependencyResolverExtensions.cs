using Domain.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using Persistence.Contexts;

namespace Membership;

public static class MembershipDependencyResolverExtensions
{
    public static IServiceCollection AddMembership(this IServiceCollection services)
    {
        services.AddIdentity<ExtendedIdentityUser, IdentityRole<int>>(action =>
        {
            action.Password.RequireNonAlphanumeric = false;
            action.Password.RequiredLength = 5;
            action.Password.RequireDigit = false;
            action.Password.RequireLowercase = false;
            action.Password.RequireUppercase = false;

            action.User.RequireUniqueEmail = true;
        })
        .AddRoles<IdentityRole<int>>()
        .AddEntityFrameworkStores<TenantDbContext>()
        .AddDefaultTokenProviders();


        services.AddScoped<IAccountService, AccountService>();

        return services;
    }
}
