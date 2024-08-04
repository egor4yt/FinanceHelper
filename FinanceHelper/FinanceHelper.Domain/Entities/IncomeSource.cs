using FinanceHelper.Domain.Common;

namespace FinanceHelper.Domain.Entities;

public class IncomeSource : ISoftDeletable
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string IncomeSourceTypeCode { get; set; }
    public string Color { get; set; }
    public long OwnerId { get; set; }

    public virtual User Owner { get; set; }
    public virtual IncomeSourceType IncomeSourceType { get; set; }
    public virtual ICollection<FinanceDistributionPlan> FinanceDistributionPlans { get; set; }
    public DateTime? DeletedAt { get; set; }
}