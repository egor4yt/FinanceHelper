using System.Collections.Generic;

namespace FinanceHelper.Api.Contracts.FinanceDistributionPlan;

/// <summary>
///     Create finance distribution plan body
/// </summary>
public class CreateFinanceDistributionPlanBody
{
    /// <summary>
    ///     Planned budget
    /// </summary>
    public decimal PlannedBudget { get; set; }

    /// <summary>
    ///     Income source id
    /// </summary>
    public long IncomeSourceId { get; set; }

    /// <summary>
    ///     Fact budget
    /// </summary>
    public decimal FactBudget { get; set; }

    /// <summary>
    ///     Planned items
    /// </summary>
    public required List<PlanItem> PlanItems { get; init; } = [];

    /// <summary>
    ///     Plan item
    /// </summary>
    public class PlanItem
    {
        /// <summary>
        ///     Step number id
        /// </summary>
        public int StepNumber { get; set; }

        /// <summary>
        ///     Planned value as string. It must be number with optional '%' symbol as last character
        /// </summary>
        public decimal PlannedValue { get; set; }

        /// <summary>
        ///     Expense item id or null if it is new expense item
        /// </summary>
        public long? ExpenseItemId { get; set; }

        /// <summary>
        ///     If expense item doesn't exist, new expense item created with that name
        /// </summary>
        public required string? NewExpenseItemName { get; set; }
    }
}