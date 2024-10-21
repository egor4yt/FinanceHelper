// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace FinanceHelper.Domain.Entities;

public class IncomeSourceType
{
    public string Code { get; set; } = null!;
    public string LocalizationKeyword { get; set; } = null!;

    public virtual ICollection<IncomeSource> IncomeSources { get; set; } = null!;
}