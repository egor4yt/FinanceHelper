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
    ///     Planned items with fixed values
    /// </summary>
    public required List<CreateFinanceDistributionPlanBodyFixedPlanItem> FixedPlanItems { get; init; } = [];
    
    /// <summary>
    ///     Planned items with floating values
    /// </summary>
    public required List<CreateFinanceDistributionPlanBodyFloatingPlanItem> FloatingPlanItems { get; init; } = [];

    /// <summary>
    ///     Plan item with fixed value
    /// </summary>
    public class CreateFinanceDistributionPlanBodyFixedPlanItem
    {
        /// <summary>
        ///     Planned value
        /// </summary>
        public decimal PlannedValue { get; set; }

        /// <summary>
        ///     Expense item id
        /// </summary>
        public long ExpenseItemId { get; set; }

        /// <summary>
        ///     Indicates that item is indivisible
        /// </summary>
        public bool Indivisible { get; set; }
    }

    /// <summary>
    ///     Plan item with floating value
    /// </summary>
    public class CreateFinanceDistributionPlanBodyFloatingPlanItem
    {
        /// <summary>
        ///     Planned value
        /// </summary>
        public decimal PlannedValue { get; set; }

        /// <summary>
        ///     Expense item id
        /// </summary>
        public long ExpenseItemId { get; set; }
    }
}