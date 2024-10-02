using FinanceHelper.Shared;
using MediatR;

namespace FinanceHelper.Application.Commands.Authorize.WithCredentials;

public class AuthorizeWithCredentialsCommandRequest : IRequest<AuthorizeWithCredentialsCommandResponse>
{
    public required string Email { get; init; }
    public required string PasswordHash { get; init; }
    public required JwtDescriptorDetails JwtDescriptorDetails { get; init; }
}