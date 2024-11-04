namespace FinanceHelper.Application.Queries.FinanceDistributionPlansTemplates.GetUser;

public class GetUserFinanceDistributionPlanTemplateQueryResponse
{
    public List<GetUserFinanceDistributionPlanTemplateQueryResponseItem> Items { get; set; } = null!;
}

public class GetUserFinanceDistributionPlanTemplateQueryResponseItem
{
    public long TemplateId { get; set; }
    public decimal PlannedBudget { get; set; }
    public string Name { get; set; } = null!;
    public GetUserFinanceDistributionPlanTemplateQueryIncomeSource IncomeSource { get; set; } = null!;
    public List<GetUserFinanceDistributionPlanTemplateQueryFixedExpenseItem> FixedExpenseItems { get; set; } = null!;
    public List<GetUserFinanceDistributionPlanTemplateQueryFloatedExpenseItem> FloatedExpenseItems { get; set; } = null!;
}

public class GetUserFinanceDistributionPlanTemplateQueryIncomeSource
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}

public class GetUserFinanceDistributionPlanTemplateQueryFixedExpenseItem
{
    // TODO: Add properties
}

public class GetUserFinanceDistributionPlanTemplateQueryFloatedExpenseItem
{
    // TODO: Add properties
}