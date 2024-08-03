namespace FinanceHelper.Domain.Entities;

public class ExpenseItemType
{
    public string Code { get; set; }
    public string LocalizationKeyword { get; set; }
    
    public virtual ICollection<ExpenseItem> ExpenseItems { get; set; }
}