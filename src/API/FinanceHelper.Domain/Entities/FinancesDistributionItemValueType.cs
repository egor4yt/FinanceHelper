// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace FinanceHelper.Domain.Entities;

public class FinancesDistributionItemValueType
{
    public string Code { get; set; } = null!;
    public string LocalizationKeyword { get; set; } = null!;

    public virtual ICollection<FinanceDistributionPlanItem> FinanceDistributionPlanItems { get; set; } = null!;
    public virtual ICollection<FinanceDistributionPlanTemplateItem> FinanceDistributionPlanTemplateItems { get; set; } = null!;
}