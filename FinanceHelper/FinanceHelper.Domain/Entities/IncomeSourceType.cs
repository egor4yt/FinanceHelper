namespace FinanceHelper.Domain.Entities;

public class IncomeSourceType
{
    public string Code { get; set; }
    public string LocalizationKeyword { get; set; }
    
    public virtual ICollection<IncomeSource> IncomeSources { get; set; }
}