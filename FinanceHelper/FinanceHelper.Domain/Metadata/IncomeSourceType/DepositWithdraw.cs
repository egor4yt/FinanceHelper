namespace FinanceHelper.Domain.Metadata;

public partial class IncomeSourceType
{
    public static readonly Entities.IncomeSourceType DepositWithdraw = new Entities.IncomeSourceType
    {
        Code = "deposit-withdraw",
        LocalizationKeyword = "DepositWithdraw"
    };
}