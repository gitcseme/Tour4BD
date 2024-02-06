using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class PackageConfiguration : IEntityTypeConfiguration<Package>
{
    public void Configure(EntityTypeBuilder<Package> builder)
    {
        builder.HasKey(p =>  p.Id);

        builder.HasOne(p => p.Comapny)
            .WithMany(c => c.Packages)
            .HasForeignKey(p => p.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
