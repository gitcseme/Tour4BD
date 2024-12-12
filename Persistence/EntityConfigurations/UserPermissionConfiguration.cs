using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class UserPermissionConfiguration : BaseEntityConfiguration<UserPermission, int>
{
    protected override void AppendConfiguration(EntityTypeBuilder<UserPermission> builder)
    {
        
    }
}