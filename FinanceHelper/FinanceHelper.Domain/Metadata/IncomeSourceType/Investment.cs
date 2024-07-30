namespace FinanceHelper.Domain.Metadata;

public partial class IncomeSourceType
{
    public static Entities.IncomeSourceType Investment = new Entities.IncomeSourceType
    {
        Code = "investment",
        LocalizationKeyword = "Investment"
    };
}