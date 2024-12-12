using Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class PermissionConfiguration : BaseEntityConfiguration<Permission, int>
{
    protected override void AppendConfiguration(EntityTypeBuilder<Permission> builder)
    {
    }
}
