using MediatR;

namespace FinanceHelper.Application.Queries.Users.GetOne;

public class GetOneUserQueryRequest : IRequest<GetOneUserQueryResponse>
{
    public long Id { get; init; }
}