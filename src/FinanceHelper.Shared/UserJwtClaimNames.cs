using Microsoft.IdentityModel.JsonWebTokens;

namespace FinanceHelper.Shared;

public static class UserJwtClaimNames
{
    /* Custom claim types */
    public const string UserId = "UserId";
    public const string UserEmail = "UserEmail";
    public const string PreferredLocalizationCode = "PreferredLocalizationCode";

    /* RFC claim types */
    public const string JsonTokenIdentifier = JwtRegisteredClaimNames.Jti;
}