using FinanceHelper.Shared;
using MediatR;

namespace FinanceHelper.Application.Commands.Users.Register;

public class RegisterUserCommandRequest : IRequest<RegisterUserCommandResponse>
{
    public string? LastName { get; init; }
    public string? FirstName { get; init; }
    public string? Email { get; init; } = string.Empty;
    public string? PasswordHash { get; init; } = string.Empty;
    public string? PreferredLocalizationCode { get; init; }
    public long? TelegramChatId { get; init; }
    public JwtDescriptorDetails? JwtDescriptorDetails { get; init; }
}