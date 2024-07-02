using MediatR;

namespace FinanceHelper.Application.Commands.Users.Register;

public class RegisterUserCommandRequest : IRequest<RegisterUserCommandResponse>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string PreferredLocalizationCode { get; set; } = string.Empty;
}