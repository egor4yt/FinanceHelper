namespace FinanceHelper.Api.Contracts.Register;

/// <summary>
///     New user details
/// </summary>
public class RegisterUserBody
{
    /// <summary>
    ///     User email
    /// </summary>
    public required string Email { get; init; } = string.Empty;

    /// <summary>
    ///     User password
    /// </summary>
    public required string Password { get; init; } = string.Empty;


    /// <summary>
    ///     Preferred localization code
    /// </summary>
    public required string? PreferredLocalizationCode { get; init; } = string.Empty;
}