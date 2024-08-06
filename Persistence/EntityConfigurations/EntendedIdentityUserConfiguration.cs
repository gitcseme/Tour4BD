using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Persistence.EntityConfigurations;

public class EntendedIdentityUserConfiguration : IEntityTypeConfiguration<ExtendedIdentityUser>
{
    public void Configure(EntityTypeBuilder<ExtendedIdentityUser> builder)
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