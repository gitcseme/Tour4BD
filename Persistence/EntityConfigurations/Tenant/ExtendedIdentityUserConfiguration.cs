using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Persistence.EntityConfigurations.Tenant;

public class ExtendedIdentityUserConfiguration : IEntityTypeConfiguration<ExtendedIdentityTenantUser>
{
    public void Configure(EntityTypeBuilder<ExtendedIdentityTenantUser> builder)
    {
        builder
            .HasMany(u => u.Permissions)
            .WithMany(p => p.ExtendedIdentityUsers)
            .UsingEntity<UserPermission>(
                r => r.HasOne(up => up.Permission).WithMany(p => p.UserPermissions).HasForeignKey(up => up.PermissionId).OnDelete(DeleteBehavior.Cascade),
                l => l.HasOne(up => up.ExtendedIdentityUser).WithMany(u => u.UserPermissions).HasForeignKey(up => up.UserId).OnDelete(DeleteBehavior.Cascade)
            );

    }
}