using FinanceHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceHelper.Persistence.Configuration.Entities;

public class TagTypeConfiguration : IEntityTypeConfiguration<TagType>
{
    public void Configure(EntityTypeBuilder<TagType> builder)
    {
        builder.HasKey(x => x.Code);

        builder
            .Property(x => x.Code)
            .HasColumnType("varchar(32)")
            .IsRequired();

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<TagType> builder)
    {
        builder.HasData(Domain.Metadata.TagType.IncomeSource);
    }
}