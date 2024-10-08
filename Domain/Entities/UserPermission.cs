﻿namespace Domain.Entities;

public class UserPermission
{
    public int UserId { get; set; }
    public int PermissionId { get; set; }

    public ExtendedIdentityTenantUser ExtendedIdentityUser { get; set; }
    public Permission Permission { get; set; }
}