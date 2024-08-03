namespace FinanceHelper.Domain.Entities;

public class FinanceDistributionPlan
{
    public long Id { get; set; }
    public decimal PlannedBudget { get; set; }
    public decimal FactBudget { get; set; }
    public DateTime CreationDate { get; set; }
    public long OwnerId { get; set; }

    public virtual ICollection<FinanceDistributionPlanItem> FinanceDistributionPlanItems { get; set; }
    public virtual User Author { get; set; }
}