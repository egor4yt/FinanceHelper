namespace FinanceHelper.Domain.Metadata;

public partial class ExpenseItemType
{
    public static Entities.ExpenseItemType Debt = new Entities.ExpenseItemType
    {
        Code = "debt",
        LocalizationKeyword = "Debt"
    };
}