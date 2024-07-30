namespace FinanceHelper.Domain.Metadata;

public partial class ExpenseItemType
{
    public static readonly Entities.ExpenseItemType Expense = new Entities.ExpenseItemType
    {
        Code = "expense",
        LocalizationKeyword = "Expense"
    };
}