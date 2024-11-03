// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

using FinanceHelper.Domain.Common;

namespace FinanceHelper.Domain.Entities;

public class ExpenseItem : ISoftDeletable
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? ExpenseItemTypeCode { get; set; }
    public string? Color { get; set; }
    public long OwnerId { get; set; }
    public bool Hidden { get; set; }

    public virtual User Owner { get; set; } = null!;
    public virtual ExpenseItemType? ExpenseItemType { get; set; }
    public virtual ICollection<FinanceDistributionPlanItem> FinanceDistributionPlanItems { get; set; } = null!;
    public virtual ICollection<FinanceDistributionPlanTemplateItem> FinanceDistributionPlanTemplateItems { get; set; } = null!;
    public DateTime? DeletedAt { get; set; }
}