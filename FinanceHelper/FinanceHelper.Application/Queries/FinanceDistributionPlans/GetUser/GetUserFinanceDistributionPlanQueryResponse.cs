﻿namespace FinanceHelper.Application.Queries.FinanceDistributionPlans.GetUser;

public class GetUserFinanceDistributionPlanQueryResponse
{
    public List<ResponseItem> Items { get; set; }
}

public class ResponseItem
{
    public long PlanId { get; set; }
    public decimal PlannedBudget { get; set; }
    public decimal FactBudget { get; set; }
    public DateTime CreatedAt { get; set; }
    public string IncomeSourceName { get; set; }
}