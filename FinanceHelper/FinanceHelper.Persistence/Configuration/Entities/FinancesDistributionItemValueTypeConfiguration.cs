using FinanceHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceHelper.Persistence.Configuration.Entities;

public class FinancesDistributionItemValueTypeConfiguration : IEntityTypeConfiguration<FinancesDistributionItemValueType>
{
    public void Configure(EntityTypeBuilder<FinancesDistributionItemValueType> builder)
    {
        builder.HasKey(x => x.Code);

        builder.HasIndex(x => new { x.Code, x.LocalizationKeyword }, "UX_Code_LocalizationKeyword")
            .IsUnique();

        builder.Property(x => x.Code)
            .HasColumnType("varchar(32)")
            .IsRequired();

        builder.Property(x => x.LocalizationKeyword)
            .HasColumnType("varchar(32)")
            .IsRequired();

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<FinancesDistributionItemValueType> builder)
    {
        builder.HasData(Domain.Metadata.FinancesDistributionItemValueType.Fixed);
        builder.HasData(Domain.Metadata.FinancesDistributionItemValueType.Floating);
    }
}