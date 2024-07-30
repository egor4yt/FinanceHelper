namespace FinanceHelper.Domain.Metadata;

public partial class ExpenseItemType
{
    public static Entities.ExpenseItemType Investment = new Entities.ExpenseItemType
    {
        Code = "investment",
        LocalizationKeyword = "Investment"
    };
}