// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace FinanceHelper.Domain.Entities;

public class User
{
    public long Id { get; set; }
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PreferredLocalizationCode { get; set; } = null!;

    public virtual SupportedLanguage PreferredLocalization { get; set; } = null!;
    public virtual ICollection<FinanceDistributionPlan> FinanceDistributionPlans { get; set; } = null!;
    public virtual ICollection<FinanceDistributionPlanTemplate> FinanceDistributionPlanTemplates { get; set; } = null!;
    public virtual ICollection<ExpenseItem> ExpenseItems { get; set; } = null!;
    public virtual ICollection<IncomeSource> IncomeSources { get; set; } = null!;
    public virtual ICollection<Tag> Tags { get; set; } = null!;
}