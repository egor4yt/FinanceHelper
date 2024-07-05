namespace FinanceHelper.Api.Services.Interfaces;

/// <summary>
///     Service for interact with current user data
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    ///     User Id
    /// </summary>
    public long UserId { get; }

    /// <summary>
    ///     Preferred user localization
    /// </summary>
    public string PreferredLocalizationCode { get; }

    /// <summary>
    ///     Is user authenticated or not
    /// </summary>
    /// <returns><see langword="true" /> if the user was authenticated, otherwise <see langword="false" /></returns>
    public bool IsAuthenticated { get; }
}