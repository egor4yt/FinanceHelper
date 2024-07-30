namespace FinanceHelper.Domain.Metadata;

public partial class ExpenseItemType
{
    public static Entities.ExpenseItemType Expense = new Entities.ExpenseItemType
    {
        Code = "expense",
        LocalizationKeyword = "Expense"
    };
}