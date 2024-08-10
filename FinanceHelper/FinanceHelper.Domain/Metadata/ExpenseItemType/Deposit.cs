namespace FinanceHelper.Domain.Metadata;

public static partial class ExpenseItemType
{
    public static readonly Entities.ExpenseItemType Deposit = new Entities.ExpenseItemType
    {
        Code = "deposit",
        LocalizationKeyword = "Deposit"
    };
}