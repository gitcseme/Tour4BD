using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public abstract class BaseEntityConfiguration<T, TId> : IEntityTypeConfiguration<T> 
    where T : EntityBase<TId>
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        AppendConfiguration(builder);
    }

    protected abstract void AppendConfiguration(EntityTypeBuilder<T> builder);
}