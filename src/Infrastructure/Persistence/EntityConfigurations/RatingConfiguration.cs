using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class RatingConfiguration : BaseEntityConfiguration<Rating, int>
{
    protected override void AppendConfiguration(EntityTypeBuilder<Rating> builder)
    {
        builder.HasOne(r => r.Package)
            .WithMany(p => p.Ratings)
            .HasForeignKey(s => s.PackageId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(r => r.Company)
            .WithMany(c => c.Ratings)
            .HasForeignKey(r => r.CompanyId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(r => r.Visitor)
            .WithMany(u => u.Ratings)
            .HasForeignKey(r => r.VisitorId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}
