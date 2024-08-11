using MediatR;

namespace FinanceHelper.Application.Commands.FinanceDistributionPlans.Create;

public class CreateFinanceDistributionPlanCommandRequest : IRequest<CreateFinanceDistributionPlanCommandResponse>
{
    public long OwnerId { get; set; }
    public long IncomeSourceId { get; set; }
    public decimal PlannedBudget { get; set; }
    public decimal FactBudget { get; set; }
    public List<PlanItem> PlanItems { get; set; }
}

public class PlanItem
{
    public int StepNumber { get; set; }
    public string PlannedValue { get; set; }
    public long? ExpenseItemId { get; set; }
    public string? NewExpenseItemName { get; set; }
}