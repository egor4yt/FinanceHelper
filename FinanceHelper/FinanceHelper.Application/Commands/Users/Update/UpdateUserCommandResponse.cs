using MediatR;

namespace FinanceHelper.Application.Commands.Users.Update;

public class UpdateUserCommandResponse
{
    public string BearerToken { get; set; } = string.Empty;
}