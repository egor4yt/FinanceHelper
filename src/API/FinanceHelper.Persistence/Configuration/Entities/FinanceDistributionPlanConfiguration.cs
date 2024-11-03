using FinanceHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceHelper.Persistence.Configuration.Entities;

public class FinanceDistributionPlanConfiguration : IEntityTypeConfiguration<FinanceDistributionPlan>
{
    public void Configure(EntityTypeBuilder<FinanceDistributionPlan> builder)
    {
        builder
            .HasOne(x => x.Owner)
            .WithMany(x => x.FinanceDistributionPlans)
            .HasForeignKey(x => x.OwnerId)
            .HasConstraintName("FK_FinanceDistributionPlan_User");

        builder
            .HasOne(x => x.IncomeSource)
            .WithMany(x => x.FinanceDistributionPlans)
            .HasForeignKey(x => x.IncomeSourceId)
            .HasConstraintName("FK_FinanceDistributionPlan_IncomeSource")
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnType("timestamptz");

        builder
            .Property(x => x.OwnerId)
            .IsRequired();

        builder
            .Property(x => x.IncomeSourceId)
            .IsRequired();

        builder
            .Property(x => x.FactBudget)
            .IsRequired()
            .HasColumnType("money");

        builder
            .Property(x => x.PlannedBudget)
            .IsRequired()
            .HasColumnType("money");
    }
}