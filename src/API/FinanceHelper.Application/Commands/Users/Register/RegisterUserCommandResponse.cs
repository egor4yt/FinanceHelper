namespace FinanceHelper.Application.Commands.Users.Register;

public class RegisterUserCommandResponse
{
    public long UserId { get; set; }
    public string BearerToken { get; set; } = null!;
}