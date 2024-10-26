using MediatR;

namespace FinanceHelper.Application.Commands.IncomeSources.Update;

public class UpdateIncomeSourceCommandRequest : IRequest
{
    public required string Name { get; init; }
    public required string Color { get; init; }
    public required string IncomeSourceTypeCode { get; init; }
    public long IncomeSourceId { get; init; }
    public long OwnerId { get; init; }
}