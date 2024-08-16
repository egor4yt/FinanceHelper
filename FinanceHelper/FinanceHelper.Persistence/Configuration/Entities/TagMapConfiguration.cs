using FinanceHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceHelper.Persistence.Configuration.Entities;

public class TagMapConfiguration : IEntityTypeConfiguration<TagMap>
{
    public void Configure(EntityTypeBuilder<TagMap> builder)
    {
        builder.HasKey(nameof(TagMap.TagId), nameof(TagMap.EntityId));

        builder
            .Property(x => x.EntityId)
            .IsRequired();

        builder
            .Property(x => x.TagId)
            .IsRequired();
    }
}