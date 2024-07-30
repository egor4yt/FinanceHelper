namespace FinanceHelper.Domain.Metadata;

public partial class IncomeSourceType
{
    public static readonly Entities.IncomeSourceType Debt = new Entities.IncomeSourceType
    {
        Code = "debt",
        LocalizationKeyword = "Debt"
    };
}