using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class ExtendedIdentityUser : IdentityUser<int>
{
    public ExtendedIdentityUser() {}

    public ExtendedIdentityUser(string email, int tenantId)
    {
        Email = UserName = email;
        TenantId = tenantId;
    }

    public int TenantId { get; set; }
}
