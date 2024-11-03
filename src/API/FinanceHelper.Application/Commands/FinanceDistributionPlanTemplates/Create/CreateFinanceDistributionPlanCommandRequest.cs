// ReSharper disable CollectionNeverUpdated.Global

using MediatR;

namespace FinanceHelper.Application.Commands.FinanceDistributionPlanTemplates.Create;

public class CreateFinanceDistributionPlanTemplateCommandRequest : IRequest<CreateFinanceDistributionPlanTemplateCommandResponse>
{
    public long OwnerId { get; init; }
    public long IncomeSourceId { get; init; }
    public decimal PlannedBudget { get; init; }
    public required List<FixedPlanTemplateItem> FixedPlanItems { get; init; }
    public required List<FloatingPlanTemplateItem> FloatingPlanItems { get; init; }
}

public class FixedPlanTemplateItem
{
    public decimal PlannedValue { get; init; }
    public bool Indivisible { get; init; }
    public long ExpenseItemId { get; init; }
}

public class FloatingPlanTemplateItem
{
    public decimal PlannedValue { get; init; }
    public long ExpenseItemId { get; init; }
}