using System.Collections.Generic;

namespace Domain.Entities;

public class Permission : BaseEntity<int>
{
    public string Name { get; set; }
    public ICollection<ExtendedIdentityUser> ExtendedIdentityUsers { get; set; }
    public ICollection<UserPermission> UserPermissions { get; set; }
}
