namespace FinanceHelper.Domain.Entities;

public class FinancesDistributionItemValueType
{
    public string Code { get; set; }
    public string LocalizationKeyword { get; set; }

    public virtual ICollection<FinanceDistributionPlanItem> FinanceDistributionPlanItems { get; set; }
}