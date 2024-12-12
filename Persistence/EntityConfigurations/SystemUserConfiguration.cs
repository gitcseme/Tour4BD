using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Persistence.EntityConfigurations;

public class SystemUserConfiguration : IEntityTypeConfiguration<SystemUser>
{
    public void Configure(EntityTypeBuilder<SystemUser> builder)
    {
        builder
            .HasMany(u => u.Permissions)
            .WithMany(p => p.SystemUsers)
            .UsingEntity<UserPermission>(
                r => r.HasOne(up => up.Permission).WithMany(p => p.UserPermissions).HasForeignKey(up => up.PermissionId).OnDelete(DeleteBehavior.Cascade),
                l => l.HasOne(up => up.SystemUser).WithMany(u => u.UserPermissions).HasForeignKey(up => up.UserId).OnDelete(DeleteBehavior.Cascade)
            );

    }
}