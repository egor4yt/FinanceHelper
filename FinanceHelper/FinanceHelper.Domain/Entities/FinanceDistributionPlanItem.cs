namespace FinanceHelper.Domain.Entities;

public class FinanceDistributionPlanItem
{
    public long Id { get; set; }
    public long FinanceDistributionPlanId { get; set; }
    public int StepNumber { get; set; }
    public decimal FactValue { get; set; }
    public decimal PlannedValue { get; set; }
    public long ExpenseItemId { get; set; }
    public string ValueTypeCode { get; set; }

    public virtual FinanceDistributionPlan FinanceDistributionPlan { get; set; }
    public virtual FinancesDistributionItemValueType ValueType { get; set; }
    public virtual ExpenseItem ExpenseItem { get; set; }
}