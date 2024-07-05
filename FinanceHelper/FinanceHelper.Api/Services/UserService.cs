using System.Security.Claims;
using FinanceHelper.Api.Services.Interfaces;
using FinanceHelper.Application.Exceptions;
using FinanceHelper.Shared;
using Microsoft.AspNetCore.Http;

namespace FinanceHelper.Api.Services;

/// <inheritdoc />
public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private readonly ClaimsPrincipal? _user = httpContextAccessor.HttpContext?.User;

    /// <inheritdoc />
    public long UserId
    {
        get
        {
            var stringUserId = _user?.FindFirstValue(UserJwtClaimNames.UserId);
            if (string.IsNullOrWhiteSpace(stringUserId)) throw new ForbiddenException("User id was null");

            var longUserId = long.Parse(stringUserId);
            return longUserId;
        }
    }

    /// <inheritdoc />
    public string PreferredLocalizationCode => _user?.FindFirstValue(UserJwtClaimNames.PreferredLocalizationCode) ?? throw new ForbiddenException("User id was null");

    /// <inheritdoc />
    public bool IsAuthenticated => httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
}