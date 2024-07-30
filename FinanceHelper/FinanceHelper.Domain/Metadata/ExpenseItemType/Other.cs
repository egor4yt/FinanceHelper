namespace FinanceHelper.Domain.Metadata;

public partial class ExpenseItemType
{
    public static readonly Entities.ExpenseItemType Other = new Entities.ExpenseItemType
    {
        Code = "other",
        LocalizationKeyword = "Other"
    };
}