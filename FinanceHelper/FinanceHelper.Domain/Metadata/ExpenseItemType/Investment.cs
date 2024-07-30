namespace FinanceHelper.Domain.Metadata;

public partial class ExpenseItemType
{
    public static readonly Entities.ExpenseItemType Investment = new Entities.ExpenseItemType
    {
        Code = "investment",
        LocalizationKeyword = "Investment"
    };
}