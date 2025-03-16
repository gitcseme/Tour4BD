using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class VisitorConfiguration : BaseEntityConfiguration<Visitor, int>
{
    protected override void AppendConfiguration(EntityTypeBuilder<Visitor> builder)
    {
        builder.HasMany(u => u.Ratings)
            .WithOne(r => r.Visitor)
            .HasForeignKey(r => r.VisitorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(u => u.Comments)
            .WithOne(cmt => cmt.Visitor)
            .HasForeignKey(cm => cm.VisitorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
