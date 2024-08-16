namespace FinanceHelper.Api.Contracts.ExpenseItem;

/// <summary>
///     Create Expense item body
/// </summary>
public class CreateExpenseItemBody
{
    /// <summary>
    ///     Expense item name
    /// </summary>
    public required string Name { get; init; } = string.Empty!;

    /// <summary>
    ///     Expense item color in HEX-format
    /// </summary>
    public required string Color { get; init; } = string.Empty;

    /// <summary>
    ///     Expense item type code
    /// </summary>
    public required string ExpenseItemTypeCode { get; init; } = string.Empty;
}