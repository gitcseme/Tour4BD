using System.Collections.Generic;

namespace Domain.Entities;

public class Permission : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public ICollection<ExtendedIdentityTenantUser> ExtendedIdentityUsers { get; set; } = [];
    public ICollection<UserPermission> UserPermissions { get; set; } = [];
}
