using FinanceHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceHelper.Persistence.Configuration.Entities;

public class FinanceDistributionPlanTemplateConfiguration : IEntityTypeConfiguration<FinanceDistributionPlanTemplate>
{
    public void Configure(EntityTypeBuilder<FinanceDistributionPlanTemplate> builder)
    {
        builder
            .HasOne(x => x.Owner)
            .WithMany(x => x.FinanceDistributionPlanTemplates)
            .HasForeignKey(x => x.OwnerId)
            .HasConstraintName("FK_FinanceDistributionPlanTemplate_User");

        builder
            .HasOne(x => x.IncomeSource)
            .WithMany(x => x.FinanceDistributionPlanTemplates)
            .HasForeignKey(x => x.IncomeSourceId)
            .HasConstraintName("FK_FinanceDistributionPlanTemplate_IncomeSource");

        builder
            .Property(x => x.OwnerId)
            .IsRequired();

        builder
            .Property(x => x.IncomeSourceId)
            .IsRequired();

        builder
            .Property(x => x.PlannedBudget)
            .IsRequired()
            .HasColumnType("money");
    }
}