namespace FinanceHelper.Application.Queries.FinanceDistributionPlans.Details;

public class DetailsFinanceDistributionPlanQueryResponse
{
    public decimal PlannedBudget { get; set; }
    public decimal FactBudget { get; set; }
    public DateTime CreatedAt { get; set; }
    public DetailsFinanceDistributionPlanQueryResponseIncomeSource IncomeSource { get; set; } = null!;
    public List<DetailsFinanceDistributionPlanQueryResponseItem> Items { get; set; } = null!;
    public Dictionary<string, string> TagsSum { get; set; } = null!;
}

public class DetailsFinanceDistributionPlanQueryResponseIncomeSource
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}

public class DetailsFinanceDistributionPlanQueryResponseItem
{
    public string PlannedValue { get; set; } = null!;
    public string FactFixedValue { get; set; } = null!;
    public ExpenseItem ExpenseItem { get; set; } = null!;
}

public class ExpenseItem
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public List<string> Tags { get; set; } = null!;
}