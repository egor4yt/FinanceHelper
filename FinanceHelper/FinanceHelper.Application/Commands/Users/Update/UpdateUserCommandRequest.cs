using FinanceHelper.Shared;
using MediatR;

namespace FinanceHelper.Application.Commands.Users.Update;

public class UpdateUserCommandRequest : IRequest<UpdateUserCommandResponse>
{
    public long Id { get; init; }
    public required string Email { get; init; } = string.Empty;
    public required string PreferredLocalizationCode { get; init; } = string.Empty;
    public JwtDescriptorDetails JwtDescriptorDetails { get; init; } = null!;
}