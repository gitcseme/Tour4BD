using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class CommentConfiguration : BaseEntityConfiguration<Comment, int>
{
    protected override void AppendConfiguration(EntityTypeBuilder<Comment> builder)
    {
        builder.HasOne(s => s.Package)
            .WithMany(p => p.Comments)
            .HasForeignKey(s => s.PackageId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(c => c.Visitor)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.VisitorId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}
