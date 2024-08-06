using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Entities;

public class ExtendedIdentityUser : IdentityUser<int>
{
    public ExtendedIdentityUser() { }

    public ExtendedIdentityUser(string email, int tenantId)
    {
        Email = UserName = email;
        TenantId = tenantId;
    }

    public int TenantId { get; set; }

    public ICollection<Permission> Permissions { get; set; }
    public ICollection<UserPermission> UserPermissions { get; set; }
}
