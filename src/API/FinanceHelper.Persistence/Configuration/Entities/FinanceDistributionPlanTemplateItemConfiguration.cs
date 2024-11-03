using FinanceHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceHelper.Persistence.Configuration.Entities;

public class FinanceDistributionPlanTemplateItemConfiguration : IEntityTypeConfiguration<FinanceDistributionPlanTemplateItem>
{
    public void Configure(EntityTypeBuilder<FinanceDistributionPlanTemplateItem> builder)
    {
        builder
            .HasOne(x => x.ExpenseItem)
            .WithMany(x => x.FinanceDistributionPlanTemplateItems)
            .HasForeignKey(x => x.ExpenseItemId)
            .HasConstraintName("FK_FinanceDistributionPlanTemplateItem_ExpenseItem")
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.ValueType)
            .WithMany(x => x.FinanceDistributionPlanTemplateItems)
            .HasForeignKey(x => x.ValueTypeCode)
            .HasConstraintName("FK_FinanceDistributionPlanTemplateItem_ValueType");

        builder
            .HasOne(x => x.FinanceDistributionPlanTemplate)
            .WithMany(x => x.FinanceDistributionPlanTemplateItems)
            .HasForeignKey(x => x.FinanceDistributionPlanTemplateId)
            .HasConstraintName("FK_FinanceDistributionPlanTemplateItem_FinanceDistributionPlanTemplate");

        builder
            .Property(x => x.ExpenseItemId)
            .IsRequired();

        builder
            .Property(x => x.PlannedValue)
            .IsRequired()
            .HasColumnType("money");

        builder
            .Property(x => x.FinanceDistributionPlanTemplateId)
            .IsRequired();
    }
}