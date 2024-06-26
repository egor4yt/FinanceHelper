namespace FinanceHelper.Api.Contracts.Register;

/// <summary>
///     Register user body
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
}