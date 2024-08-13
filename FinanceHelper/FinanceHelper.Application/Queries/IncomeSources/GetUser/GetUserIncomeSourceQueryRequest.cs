using MediatR;

namespace FinanceHelper.Application.Queries.IncomeSources.GetUser;

public class GetUserIncomeSourceQueryRequest : IRequest<GetUserIncomeSourceQueryResponse>
{
    public long OwnerId { get; set; }
    public string LocalizationCode { get; set; }
}