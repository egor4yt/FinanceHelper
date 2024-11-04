using System.Collections.Generic;

// ReSharper disable CollectionNeverUpdated.Global

namespace FinanceHelper.Api.Contracts.FinanceDistributionPlanTemplate;

/// <summary>
///     Create finance distribution plan template body
/// </summary>
public class CreateFinanceDistributionPlanTemplateBody
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
    ///     Template name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Planned items with fixed values
    /// </summary>
    public required List<CreateFinanceDistributionPlanTemplateBodyFixedPlanItem> FixedPlanItems { get; init; } = [];

    /// <summary>
    ///     Planned items with floating values
    /// </summary>
    public required List<CreateFinanceDistributionPlanTemplateBodyFloatingPlanItem> FloatingPlanItems { get; init; } = [];

    /// <summary>
    ///     Plan item with fixed value
    /// </summary>
    public class CreateFinanceDistributionPlanTemplateBodyFixedPlanItem
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
    public class CreateFinanceDistributionPlanTemplateBodyFloatingPlanItem
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