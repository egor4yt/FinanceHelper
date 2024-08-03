using System.Collections.Generic;

namespace FinanceHelper.Api.Contracts.FinanceDistributionPlan;

public class CreateFinanceDistributionPlanBody
{
    public decimal PlannedBudget { get; set; }
    public decimal FactBudget { get; set; }
    public List<PlanItem> PlanItems { get; set; }

    public class PlanItem
    {
        public int StepNumber { get; set; }
        public decimal PlannedValue { get; set; }
        public long ExpenseItemId { get; set; }
        public string ValueTypeCode { get; set; }
    }
}