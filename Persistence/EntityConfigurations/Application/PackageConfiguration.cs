using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations.Application;

public class PackageConfiguration : IEntityTypeConfiguration<Package>
{
    public void Configure(EntityTypeBuilder<Package> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Comapny)
            .WithMany(c => c.Packages)
            .HasForeignKey(p => p.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(p => p.Discounts)
            .WithOne(d => d.Package)
            .HasForeignKey(d => d.PackageId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(p => p.Spots)
            .WithOne(s => s.Package)
            .HasForeignKey(s => s.PackageId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(p => p.Comments)
            .WithOne(cmt => cmt.Package)
            .HasForeignKey(cm => cm.PackageId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(p => p.Ratings)
            .WithOne(r => r.Package)
            .HasForeignKey(r => r.PackageId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
