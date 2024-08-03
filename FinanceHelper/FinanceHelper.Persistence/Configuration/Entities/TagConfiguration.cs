using FinanceHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceHelper.Persistence.Configuration.Entities;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder
            .HasOne(x => x.TagType)
            .WithMany(x => x.Tags)
            .HasForeignKey(x => x.TagTypeCode)
            .HasConstraintName("FK_Tag_TagType");

        builder
            .Property(x => x.TagTypeCode)
            .IsRequired()
            .HasColumnType("varchar(32)");
    }
}