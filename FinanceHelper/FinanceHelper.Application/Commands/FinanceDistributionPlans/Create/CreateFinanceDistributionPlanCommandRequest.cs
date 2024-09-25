// ReSharper disable CollectionNeverUpdated.Global

using MediatR;

namespace FinanceHelper.Application.Commands.FinanceDistributionPlans.Create;

public class CreateFinanceDistributionPlanCommandRequest : IRequest<CreateFinanceDistributionPlanCommandResponse>
{
    public long OwnerId { get; init; }
    public long IncomeSourceId { get; init; }
    public decimal PlannedBudget { get; init; }
    public decimal FactBudget { get; init; }
    public required List<FixedPlanItem> FixedPlanItems { get; init; }
    public required List<FloatingPlanItem> FloatingPlanItems { get; init; }
}

public class FixedPlanItem
{
    public decimal PlannedValue { get; init; }
    public bool Indivisible { get; init; }
    public long ExpenseItemId { get; init; }
}

public class FloatingPlanItem
{
    public decimal PlannedValue { get; init; }
    public long ExpenseItemId { get; init; }
}