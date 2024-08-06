using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.HasKey(s => s.Id);

        builder.HasOne(s => s.Package)
            .WithMany(p => p.Discounts)
            .HasForeignKey(s => s.PackageId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
