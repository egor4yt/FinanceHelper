using FinanceHelper.Domain.Entities;

namespace FinanceHelper.Shared;

public class UserJwtDetails(User user)
{
    public long Id { get; set; } = user.Id;
    public string Email { get; set; } = user.Email;
    public string PreferredLocalizationCode { get; set; } = user.PreferredLocalizationCode;
}