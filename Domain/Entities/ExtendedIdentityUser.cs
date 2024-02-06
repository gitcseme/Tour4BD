using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class ExtendedIdentityUser : IdentityUser<int>
{
    public int TenantId { get; set; }
}
