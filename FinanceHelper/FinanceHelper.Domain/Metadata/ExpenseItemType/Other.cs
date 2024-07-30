namespace FinanceHelper.Domain.Metadata;

public partial class ExpenseItemType
{
    public static Entities.ExpenseItemType Other = new Entities.ExpenseItemType
    {
        Code = "other",
        LocalizationKeyword = "Other"
    };
}