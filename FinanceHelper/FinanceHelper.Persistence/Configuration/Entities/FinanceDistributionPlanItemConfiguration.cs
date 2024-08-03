using FinanceHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceHelper.Persistence.Configuration.Entities;

public class FinanceDistributionPlanItemConfiguration : IEntityTypeConfiguration<FinanceDistributionPlanItem>
{
    public void Configure(EntityTypeBuilder<FinanceDistributionPlanItem> builder)
    {
        builder
            .HasOne(x => x.ExpenseItem)
            .WithMany(x => x.FinanceDistributionPlanItems)
            .HasForeignKey(x => x.ExpenseItemId)
            .HasConstraintName("FK_FinanceDistributionPlanItem_ExpenseItem");
        
        builder
            .HasOne(x => x.ValueType)
            .WithMany(x => x.FinanceDistributionPlanItems)
            .HasForeignKey(x => x.ValueTypeCode)
            .HasConstraintName("FK_FinanceDistributionPlanItem_ValueType");
        
        builder
            .HasOne(x => x.FinanceDistributionPlan)
            .WithMany(x => x.FinanceDistributionPlanItems)
            .HasForeignKey(x => x.FinanceDistributionPlanId)
            .HasConstraintName("FK_FinanceDistributionPlanItem_FinanceDistributionPlan");

        builder
            .Property(x => x.ExpenseItemId)
            .IsRequired();

        builder
            .Property(x => x.FactValue)
            .IsRequired()
            .HasColumnType("money");
        
        builder
            .Property(x => x.PlannedValue)
            .IsRequired()
            .HasColumnType("money");

        builder
            .Property(x => x.StepNumber)
            .IsRequired();

        builder
            .Property(x => x.FinanceDistributionPlanId)
            .IsRequired();
    }
}