namespace FinanceHelper.Api.Contracts.ExpenseItem;

/// <summary>
///     Create Expense item body
/// </summary>
public class CreateExpenseItemBody
{
    /// <summary>
    ///     Expense item name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Expense item color in HEX-format
    /// </summary>
    public string Color { get; set; }

    /// <summary>
    ///     Expense item type code
    /// </summary>
    public string ExpenseItemTypeCode { get; set; }
}