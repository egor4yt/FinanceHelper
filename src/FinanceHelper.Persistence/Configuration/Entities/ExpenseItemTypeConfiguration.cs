using FinanceHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceHelper.Persistence.Configuration.Entities;

public class ExpenseItemTypeConfiguration : IEntityTypeConfiguration<ExpenseItemType>
{
    public void Configure(EntityTypeBuilder<ExpenseItemType> builder)
    {
        builder.HasKey(x => x.Code);

        builder
            .Property(x => x.Code)
            .HasColumnType("varchar(32)")
            .IsRequired();

        builder
            .Property(x => x.LocalizationKeyword)
            .HasColumnType("varchar(32)")
            .IsRequired();

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<ExpenseItemType> builder)
    {
        builder.HasData(Domain.Metadata.ExpenseItemType.Expense);
        builder.HasData(Domain.Metadata.ExpenseItemType.Charity);
        builder.HasData(Domain.Metadata.ExpenseItemType.Debt);
        builder.HasData(Domain.Metadata.ExpenseItemType.Investment);
        builder.HasData(Domain.Metadata.ExpenseItemType.Other);
        builder.HasData(Domain.Metadata.ExpenseItemType.Deposit);
    }
}