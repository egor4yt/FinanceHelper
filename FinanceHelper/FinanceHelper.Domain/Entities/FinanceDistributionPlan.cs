// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace FinanceHelper.Domain.Entities;

public class FinanceDistributionPlan
{
    public long Id { get; set; }
    public decimal PlannedBudget { get; set; }
    public decimal FactBudget { get; set; }
    public DateTime CreatedAt { get; set; }
    public long OwnerId { get; set; }
    public long IncomeSourceId { get; set; }

    public virtual ICollection<FinanceDistributionPlanItem> FinanceDistributionPlanItems { get; set; } = null!;
    public virtual User Owner { get; set; } = null!;
    public virtual IncomeSource IncomeSource { get; set; } = null!;
}