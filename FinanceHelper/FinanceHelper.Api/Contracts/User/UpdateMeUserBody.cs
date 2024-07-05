namespace FinanceHelper.Api.Contracts.User;

/// <summary>
///     Updated user details
/// </summary>
public class UpdateMeUserBody
{
    /// <summary>
    ///     User email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Preferred localization code
    /// </summary>
    public string PreferredLocalization { get; set; }
}