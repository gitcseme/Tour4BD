using System.Collections.Generic;

namespace Domain.Entities;

public class Permission : EntityBase<int>
{
    public string Name { get; set; } = string.Empty;

    public ICollection<SystemUser> SystemUsers { get; set; } = [];
    public ICollection<UserPermission> UserPermissions { get; set; } = [];
}
