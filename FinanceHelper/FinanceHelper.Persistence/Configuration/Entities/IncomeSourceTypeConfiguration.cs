using FinanceHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceHelper.Persistence.Configuration.Entities;

public class IncomeSourceTypeConfiguration : IEntityTypeConfiguration<IncomeSourceType>
{
    public void Configure(EntityTypeBuilder<IncomeSourceType> builder)
    {
        builder.HasKey(x => x.Code);

        builder
            .HasIndex(x => new { x.Code, x.LocalizationKeyword }, "UX_Code_LocalizationKeyword")
            .IsUnique();

        builder.Property(x => x.Code)
            .HasColumnType("varchar(32)")
            .IsRequired();

        builder.Property(x => x.LocalizationKeyword)
            .HasColumnType("varchar(32)")
            .IsRequired();

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<IncomeSourceType> builder)
    {
        builder.HasData(Domain.Metadata.IncomeSourceType.Debt);
        builder.HasData(Domain.Metadata.IncomeSourceType.Other);
        builder.HasData(Domain.Metadata.IncomeSourceType.Investment);
        builder.HasData(Domain.Metadata.IncomeSourceType.Work);
        builder.HasData(Domain.Metadata.IncomeSourceType.DepositWithdraw);
    }
}