using MediatR;

namespace FinanceHelper.Application.Commands.Tags.Attach;

public class AttachTagCommandRequest : IRequest
{
    public long EntityId { get; init; }
    public long TagId { get; init; }
    public long OwnerId { get; init; }
    public required string TagTypeCode { get; init; }
}