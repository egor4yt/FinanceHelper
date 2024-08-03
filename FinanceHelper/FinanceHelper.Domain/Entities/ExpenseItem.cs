using FinanceHelper.Domain.Common;

namespace FinanceHelper.Domain.Entities;

public class ExpenseItem : ISoftDeletable
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string ExpenseItemTypeCode { get; set; }
    public string Color { get; set; }
    public virtual ExpenseItemType ExpenseItemType { get; set; }
    public DateTime? DeletedAt { get; set; }
}