namespace FinanceHelper.Domain.Metadata;

public static partial class ExpenseItemType
{
    public static readonly Entities.ExpenseItemType Debt = new Entities.ExpenseItemType
    {
        Code = "debt",
        LocalizationKeyword = "Debt"
    };
}