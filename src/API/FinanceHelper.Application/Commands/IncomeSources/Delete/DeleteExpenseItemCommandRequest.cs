using MediatR;

namespace FinanceHelper.Application.Commands.IncomeSources.Delete;

public class DeleteIncomeSourceCommandRequest : IRequest
{
    public long IncomeSourceId { get; init; }
    public long OwnerId { get; init; }
}