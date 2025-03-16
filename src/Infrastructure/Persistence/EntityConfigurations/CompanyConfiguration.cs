using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class CompanyConfiguration : BaseEntityConfiguration<Company, int>
{
    protected override void AppendConfiguration(EntityTypeBuilder<Company> builder)
    {
        builder.HasMany(c => c.Employees)
            .WithOne(e => e.Company)
            .HasForeignKey(e => e.CompanyId);

        builder
            .HasMany(c => c.Packages)
            .WithOne(p => p.Comapny)
            .HasForeignKey(p => p.CompanyId);

        builder
            .HasMany(c => c.Ratings)
            .WithOne(r => r.Company)
            .HasForeignKey(r => r.CompanyId);

        builder
            .HasOne(c => c.TravelAgency)
            .WithMany(t => t.Companies)
            .HasForeignKey(c => c.TravelAgencyId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
