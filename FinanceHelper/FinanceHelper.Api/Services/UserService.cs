using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FinanceHelper.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace FinanceHelper.Api.Services;

/// <inheritdoc />
public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    /// <inheritdoc />
    public long? UserId { get; } = long.Parse(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "-1");

    /// <inheritdoc />
    public bool IsAuthenticated => UserId != -1;

    /// <inheritdoc />
    public async Task AuthenticateWithCookieAsync(long userId)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = null,
            IssuedUtc = DateTime.UtcNow
        };

        if (httpContextAccessor.HttpContext == null) return;

        await httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }
}