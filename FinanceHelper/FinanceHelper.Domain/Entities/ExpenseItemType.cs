// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace FinanceHelper.Domain.Entities;

public class ExpenseItemType
{
    public string Code { get; set; } = null!;
    public string LocalizationKeyword { get; set; } = null!;

    public virtual ICollection<ExpenseItem> ExpenseItems { get; set; } = null!;
}