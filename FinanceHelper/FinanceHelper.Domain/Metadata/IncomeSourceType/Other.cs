namespace FinanceHelper.Domain.Metadata;

public partial class IncomeSourceType
{
    public static readonly Entities.IncomeSourceType Other = new Entities.IncomeSourceType
    {
        Code = "other",
        LocalizationKeyword = "Other"
    };
}