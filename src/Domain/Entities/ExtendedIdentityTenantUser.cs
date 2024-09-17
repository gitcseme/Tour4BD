using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Entities;

public class ExtendedIdentityTenantUser : IdentityUser<int>
{
    public ExtendedIdentityTenantUser() { }

    public ExtendedIdentityTenantUser(string email, int tenantId)
    {
        Email = UserName = email;
        TenantId = tenantId;
    }

    public int TenantId { get; set; }

    public ICollection<Permission> Permissions { get; set; } = [];
    public ICollection<UserPermission> UserPermissions { get; set; } = [];
}
