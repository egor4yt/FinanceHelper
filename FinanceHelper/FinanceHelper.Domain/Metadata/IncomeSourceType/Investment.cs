namespace FinanceHelper.Domain.Metadata;

public partial class IncomeSourceType
{
    public static readonly Entities.IncomeSourceType Investment = new Entities.IncomeSourceType
    {
        Code = "investment",
        LocalizationKeyword = "Investment"
    };
}