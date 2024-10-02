using FinanceHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceHelper.Persistence.Configuration.Entities;

public class MetadataTypeConfiguration : IEntityTypeConfiguration<MetadataType>
{
    public void Configure(EntityTypeBuilder<MetadataType> builder)
    {
        builder.HasKey(x => x.Code);

        builder
            .Property(x => x.Code)
            .HasColumnType("varchar(64)")
            .IsRequired();

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<MetadataType> builder)
    {
        builder.HasData(Domain.Metadata.MetadataType.ExpenseItemType);
        builder.HasData(Domain.Metadata.MetadataType.IncomeSourceType);
        builder.HasData(Domain.Metadata.MetadataType.FinancesDistributionItemValueType);
    }
}