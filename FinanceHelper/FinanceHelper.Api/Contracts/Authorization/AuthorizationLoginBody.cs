namespace FinanceHelper.Api.Contracts.Authorization;

/// <summary>
///     User credentials data for authorization
/// </summary>
public class AuthorizationLoginBody
{
    /// <summary>
    ///     User email
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    ///     User password
    /// </summary>
    public string Password { get; set; } = null!;
}