namespace FinanceHelper.Application.Queries.Users;

public class GetOneUserQueryResponse
{
    public long Id { get; set; }
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PreferredLocalizationCode { get; set; } = null!;
}