// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace FinanceHelper.Domain.Entities;

public class FinanceDistributionPlanTemplate
{
    public long Id { get; set; }
    public decimal PlannedBudget { get; set; }
    public long OwnerId { get; set; }
    public long IncomeSourceId { get; set; }

    public virtual ICollection<FinanceDistributionPlanTemplateItem> FinanceDistributionPlanTemplateItems { get; set; } = null!;
    public virtual User Owner { get; set; } = null!;
    public virtual IncomeSource IncomeSource { get; set; } = null!;
}