using FinanceHelper.Shared;
using MediatR;

namespace FinanceHelper.Application.Commands.Authorize.WithCredentials;

public class AuthorizeWithCredentialsCommandRequest : IRequest<AuthorizeWithCredentialsCommandResponse>
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public JwtDescriptorDetails JwtDescriptorDetails { get; set; } = null!;
}