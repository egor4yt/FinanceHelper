using FinanceHelper.Shared;
using MediatR;

namespace FinanceHelper.Application.Commands.Users.Register;

public class RegisterUserCommandRequest : IRequest<RegisterUserCommandResponse>
{
    public required string LastName { get; init; } = string.Empty;
    public required string FirstName { get; init; } = string.Empty;
    public required string Email { get; init; } = string.Empty;
    public required string PasswordHash { get; init; } = string.Empty;
    public required string PreferredLocalizationCode { get; init; } = string.Empty;
    public JwtDescriptorDetails JwtDescriptorDetails { get; init; } = null!;
}