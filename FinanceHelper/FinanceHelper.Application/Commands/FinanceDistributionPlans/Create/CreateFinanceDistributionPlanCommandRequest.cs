using MediatR;

namespace FinanceHelper.Application.Commands.FinanceDistributionPlans.Create;

public class CreateFinanceDistributionPlanCommandRequest : IRequest<CreateFinanceDistributionPlanCommandResponse>
{
    public long OwnerId { get; init; }
    public long IncomeSourceId { get; init; }
    public decimal PlannedBudget { get; init; }
    public decimal FactBudget { get; init; }
    public required List<PlanItem> PlanItems { get; init; }
}

public class PlanItem
{
    public int StepNumber { get; init; }
    public decimal PlannedValue { get; init; }
    public string PlannedValueTypeCode { get; init; } = string.Empty;
    public long? ExpenseItemId { get; init; }
    public string? NewExpenseItemName { get; init; }
}