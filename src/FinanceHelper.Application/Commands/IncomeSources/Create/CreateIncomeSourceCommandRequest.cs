using MediatR;

namespace FinanceHelper.Application.Commands.IncomeSources.Create;

public class CreateIncomeSourceCommandRequest : IRequest<CreateIncomeSourceCommandResponse>
{
    public required string Name { get; init; } = string.Empty;
    public required string Color { get; init; } = string.Empty;
    public required string IncomeSourceTypeCode { get; init; } = string.Empty;
    public long OwnerId { get; init; }
}