using Application;
using Domain.Entities;

using Microsoft.AspNetCore.Identity;

namespace Membership;

public class AccountService : IAccountService
{
    private readonly UserManager<ExtendedIdentityUser> _userManager;

    public AccountService(UserManager<ExtendedIdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ExtendedIdentityUser> CreateUser(string email, string password, int tenantId)
    {
        var user = new ExtendedIdentityUser(email, tenantId);
        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            return await Task.FromResult(user);
        }

        throw new Exception("Error creating user");
    }

    public async Task<ExtendedIdentityUser> CreateUserAndAssignAdminRoleAsync(string email, string password, int tenantId)
    {
        var user = await CreateUser(email, password, tenantId);
        await _userManager.AddToRoleAsync(user, AppConstants.Roles.Admin);
        return user;
    }
}
