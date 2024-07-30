namespace FinanceHelper.Domain.Metadata;

public partial class IncomeSourceType
{
    public static Entities.IncomeSourceType DepositWithdraw = new Entities.IncomeSourceType
    {
        Code = "deposit-withdraw",
        LocalizationKeyword = "DepositWithdraw"
    };
}