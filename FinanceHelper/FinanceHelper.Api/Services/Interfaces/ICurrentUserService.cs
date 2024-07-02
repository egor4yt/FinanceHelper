using System.Threading.Tasks;

namespace FinanceHelper.Api.Services.Interfaces;

/// <summary>
///     Service for interact with current user data
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    ///     User Id
    /// </summary>
    public long? UserId { get; }

    /// <summary>
    ///     Authenticated user
    /// </summary>
    public bool IsAuthenticated { get; }
}