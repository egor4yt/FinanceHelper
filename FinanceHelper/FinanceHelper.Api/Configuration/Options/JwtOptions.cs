using FinanceHelper.Shared;

namespace FinanceHelper.Api.Configuration.Options;

/// <summary>
///     Json web tokens options
/// </summary>
public class JwtOptions
{
    /// <summary>
    ///     JWT issuer
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    ///     JWT audience
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    ///     JWT secret key
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    ///     JWT lifetime
    /// </summary>
    public int TokenLifetimeInHours { get; set; }

    /// <summary>
    ///     Creates an instance of the <see cref="JwtDescriptorDetails" />
    /// </summary>
    /// <returns>An instance of the <see cref="JwtDescriptorDetails" /></returns>
    public JwtDescriptorDetails ToJwtDescriptorDetails()
    {
        var response = new JwtDescriptorDetails();

        response.Issuer = Issuer;
        response.Audience = Audience;
        response.Key = Key;
        response.TokenLifetimeInHours = TokenLifetimeInHours;

        return response;
    }
}