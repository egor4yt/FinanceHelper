using MediatR;

namespace FinanceHelper.Application.Queries.IncomeSources.GetUser;

public class GetUserIncomeSourceQueryRequest : IRequest<GetUserIncomeSourceQueryResponse>
{
    public long OwnerId { get; init; }
    public required string LocalizationCode { get; init; }
}