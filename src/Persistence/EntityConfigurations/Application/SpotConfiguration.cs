using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Persistence.EntityConfigurations.Application;

public class SpotConfiguration : IEntityTypeConfiguration<Spot>
{
    public void Configure(EntityTypeBuilder<Spot> builder)
    {
        builder.HasKey(s => s.Id);

        builder.HasOne(s => s.Package)
            .WithMany(p => p.Spots)
            .HasForeignKey(s => s.PackageId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}
