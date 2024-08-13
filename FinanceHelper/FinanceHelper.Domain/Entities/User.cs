﻿namespace FinanceHelper.Domain.Entities;

public class User
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PreferredLocalizationCode { get; set; }

    public virtual SupportedLanguage PreferredLocalization { get; set; }
    public virtual ICollection<FinanceDistributionPlan> FinanceDistributionPlans { get; set; }
    public virtual ICollection<ExpenseItem> ExpenseItems { get; set; }
    public virtual ICollection<IncomeSource> IncomeSources { get; set; }
}