namespace FinanceHelper.Application.Queries.FinanceDistributionPlans.Details;

public class DetailsFinanceDistributionPlanQueryResponse
{
    public decimal PlannedBudget { get; set; }
    public decimal FactBudget { get; set; }
    public DateTime CreatedAt { get; set; }
    public IncomeSource IncomeSource { get; set; }
    public List<StepGroup> Steps { get; set; }
}

public class IncomeSource
{
    public long Id { get; set; }
    public string Name { get; set; }
}

public class StepGroup
{
    public decimal StepFactBudget { get; set; }
    public int StepNumber { get; set; }
    public List<StepItem> Items { get; set; }
}

public class StepItem
{
    public decimal PlannedValue { get; set; }
    public string PlannedValuePostfix { get; set; }
    public decimal FactFixedValue { get; set; }
    public ExpenseItem ExpenseItem { get; set; }
}

public class ExpenseItem
{
    public long Id { get; set; }
    public string Name { get; set; }
}