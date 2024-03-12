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

    public async Task<int> CreateUser(string username, string password, int tenantId)
    {
        var user = new ExtendedIdentityUser(username, tenantId);
        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            return await Task.FromResult(user.Id);
        }

        throw new Exception("Error creating user");
    }
}
