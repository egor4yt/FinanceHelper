namespace FinanceHelper.Api.Contracts.Register;

/// <summary>
///     New user details
/// </summary>
public class RegisterUserBody
{
    /// <summary>
    ///     User email
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    ///     User password
    /// </summary>
    public string Password { get; set; } = string.Empty;


    /// <summary>
    ///     Preferred localization code
    /// </summary>
    public string? PreferredLocalizationCode { get; set; } = string.Empty;
}