namespace FinanceHelper.Api.Contracts.User;

/// <summary>
///     Updated user details
/// </summary>
public class UpdateMeUserBody
{
    /// <summary>
    ///     User email
    /// </summary>
    public required string Email { get; init; } = string.Empty;

    /// <summary>
    ///     Preferred localization code
    /// </summary>
    public required string PreferredLocalization { get; init; } = string.Empty;
}