namespace FinanceHelper.Domain.Entities;

public class User
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PreferredLocalizationCode { get; set; }
}