// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

using FinanceHelper.Domain.Common;

namespace FinanceHelper.Domain.Entities;

public class IncomeSource : ISoftDeletable
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string IncomeSourceTypeCode { get; set; } = null!;
    public string Color { get; set; } = null!;
    public long OwnerId { get; set; }

    public virtual User Owner { get; set; } = null!;
    public virtual IncomeSourceType IncomeSourceType { get; set; } = null!;
    public virtual ICollection<FinanceDistributionPlan> FinanceDistributionPlans { get; set; } = null!;
    public virtual ICollection<FinanceDistributionPlanTemplate> FinanceDistributionPlanTemplates { get; set; } = null!;
    public DateTime? DeletedAt { get; set; }
}