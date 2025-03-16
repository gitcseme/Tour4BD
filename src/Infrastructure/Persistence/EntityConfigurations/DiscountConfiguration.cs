using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class DiscountConfiguration : BaseEntityConfiguration<Discount, int>
{
    protected override void AppendConfiguration(EntityTypeBuilder<Discount> builder)
    {
        builder.HasOne(s => s.Package)
            .WithMany(p => p.Discounts)
            .HasForeignKey(s => s.PackageId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
