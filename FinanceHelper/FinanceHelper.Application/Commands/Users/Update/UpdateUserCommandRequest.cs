using FinanceHelper.Shared;
using MediatR;

namespace FinanceHelper.Application.Commands.Users.Update;

public class UpdateUserCommandRequest : IRequest<UpdateUserCommandResponse>
{
    public long Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PreferredLocalizationCode { get; set; } = string.Empty;
    public JwtDescriptorDetails JwtDescriptorDetails { get; set; } = null!;
}