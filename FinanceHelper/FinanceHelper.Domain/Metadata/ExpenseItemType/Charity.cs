namespace FinanceHelper.Domain.Metadata;

public partial class ExpenseItemType
{
    public static Entities.ExpenseItemType Charity = new Entities.ExpenseItemType
    {
        Code = "charity",
        LocalizationKeyword = "Charity"
    };
}