namespace FinanceHelper.Api.Contracts.ExpenseItem;

/// <summary>
///     Update expense item body
/// </summary>
public class UpdateExpenseItemBody
{
    /// <summary>
    ///     New expense item name
    /// </summary>
    public string Name { get; init; } = string.Empty!;

    /// <summary>
    ///     New expense item color
    /// </summary>
    public string Color { get; init; } = string.Empty!;

    /// <summary>
    ///     New expense type code
    /// </summary>
    public string ExpenseItemTypeCode { get; init; } = string.Empty!;
}