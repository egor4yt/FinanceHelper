﻿namespace FinanceHelper.Api.Contracts.Register;

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
    ///     User first name
    /// </summary>
    public required string FirstName { get; init; } = string.Empty;

    /// <summary>
    ///     User last name
    /// </summary>
    public required string LastName { get; init; } = string.Empty;

    /// <summary>
    ///     User password
    /// </summary>
    public required string Password { get; init; } = string.Empty;
}