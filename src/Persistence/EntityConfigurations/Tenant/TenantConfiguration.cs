using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations.Tenant;

public class TenantConfiguration : IEntityTypeConfiguration<Domain.Entities.Tenant>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Tenant> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.ConnectionString).IsRequired();
        builder.Property(t => t.OrganizationName).IsRequired();
    }
}