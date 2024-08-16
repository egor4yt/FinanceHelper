namespace FinanceHelper.Api.Contracts.Authorization;

/// <summary>
///     User credentials data for authorization
/// </summary>
public class AuthorizationLoginBody
{
    /// <summary>
    ///     User email
    /// </summary>
    public required string Email { get; init; } = string.Empty;

    /// <summary>
    ///     User password
    /// </summary>
    public required string Password { get; init; } = string.Empty;
}