using Common.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Shared.Abstractions;

public abstract class BaseEntityTypeConfiguration<TEntity>
    : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.CreatedAt)
            .HasDefaultValueSql(DatabaseConstants.GetDate);

        builder
            .Property(x => x.UpdatedAt)
            .HasDefaultValueSql(DatabaseConstants.GetDate);

        builder
            .Property(x => x.IsDeleted)
            .HasDefaultValue(value: false);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
