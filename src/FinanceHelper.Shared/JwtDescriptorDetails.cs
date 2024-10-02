namespace FinanceHelper.Shared;

public class JwtDescriptorDetails
{
    public string Key { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public int TokenLifetimeInHours { get; set; }
}