﻿using FinanceHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceHelper.Persistence.Configuration.Entities;

public class ExpenseItemConfiguration : IEntityTypeConfiguration<ExpenseItem>
{
    public void Configure(EntityTypeBuilder<ExpenseItem> builder)
    {
        builder
            .HasOne(x => x.ExpenseItemType)
            .WithMany(x => x.ExpenseItems)
            .HasForeignKey(x => x.ExpenseItemTypeCode)
            .HasConstraintName("FK_ExpenseItem_ExpenseItemType");

        builder
            .HasOne(x => x.Owner)
            .WithMany(x => x.ExpenseItems)
            .HasForeignKey(x => x.OwnerId)
            .HasConstraintName("FK_IncomeSource_User");

        builder
            .Property(x => x.Color)
            .HasComment("HEX-format color")
            .HasColumnType("varchar(7)");

        builder
            .Property(x => x.Hidden)
            .IsRequired()
            .HasComment("Indicates that the expense item created while user creating a finance distribution plan or other automated way")
            .HasColumnType("boolean");

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar(32)");

        builder
            .Property(x => x.DeletedAt)
            .HasColumnType("timestamptz");

        builder
            .Property(x => x.ExpenseItemTypeCode)
            .HasColumnType("varchar(32)");
    }
}