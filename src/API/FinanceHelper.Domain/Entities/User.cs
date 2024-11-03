// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace FinanceHelper.Domain.Entities;

public class User
{
    public long Id { get; set; }
    public string? Email { get; set; }
    public long? TelegramChatId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PasswordHash { get; set; }
    public string? PreferredLocalizationCode { get; set; }

    public virtual SupportedLanguage? PreferredLocalization { get; set; }
    public virtual ICollection<FinanceDistributionPlan> FinanceDistributionPlans { get; set; } = null!;
    public virtual ICollection<ExpenseItem> ExpenseItems { get; set; } = null!;
    public virtual ICollection<IncomeSource> IncomeSources { get; set; } = null!;
    public virtual ICollection<Tag> Tags { get; set; } = null!;
}