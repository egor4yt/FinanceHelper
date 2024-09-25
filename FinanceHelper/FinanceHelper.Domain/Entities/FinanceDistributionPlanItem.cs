// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace FinanceHelper.Domain.Entities;

public class FinanceDistributionPlanItem
{
    public long Id { get; set; }
    public long FinanceDistributionPlanId { get; set; }
    public decimal PlannedValue { get; set; }
    public long ExpenseItemId { get; set; }
    public string ValueTypeCode { get; set; } = null!;

    public virtual FinanceDistributionPlan FinanceDistributionPlan { get; set; } = null!;
    public virtual FinancesDistributionItemValueType ValueType { get; set; } = null!;
    public virtual ExpenseItem ExpenseItem { get; set; } = null!;
}