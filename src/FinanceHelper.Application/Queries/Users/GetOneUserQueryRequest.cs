using MediatR;

namespace FinanceHelper.Application.Queries.Users;

public class GetOneUserQueryRequest : IRequest<GetOneUserQueryResponse>
{
    public long Id { get; init; }
}