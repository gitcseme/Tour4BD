using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Persistence.EntityConfigurations;

public class SpotConfiguration : BaseEntityConfiguration<Spot, int>
{
    protected override void AppendConfiguration(EntityTypeBuilder<Spot> builder)
    {
        builder.HasOne(s => s.Package)
            .WithMany(p => p.Spots)
            .HasForeignKey(s => s.PackageId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}
