﻿using System.Collections.Generic;

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
    ///     Fact budget
    /// </summary>
    public decimal FactBudget { get; set; }
    
    /// <summary>
    ///     Planned items
    /// </summary>
    public List<PlanItem> PlanItems { get; set; }

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
        public string PlannedValue { get; set; }
        
        /// <summary>
        ///     Expense item id
        /// </summary>
        public long ExpenseItemId { get; set; }
    }
}