using FinanceHelper.Shared;
using MediatR;

namespace FinanceHelper.Application.Commands.Users.Register;

public class RegisterUserCommandRequest : IRequest<RegisterUserCommandResponse>
{
    public string? LastName { get; init; }
    public string? FirstName { get; init; }
    public string? Email { get; init; }
    public string? PasswordHash { get; init; }
    public string? PreferredLocalizationCode { get; init; }
    public long? TelegramChatId { get; init; }
    public JwtDescriptorDetails? JwtDescriptorDetails { get; init; }
}