using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Entities;

public class SystemUser : IdentityUser<int>
{
    public SystemUser() { }

    public SystemUser(string email)
    {
        Email = UserName = email;
    }


    public ICollection<Permission> Permissions { get; set; } = [];
    public ICollection<UserPermission> UserPermissions { get; set; } = [];
}
