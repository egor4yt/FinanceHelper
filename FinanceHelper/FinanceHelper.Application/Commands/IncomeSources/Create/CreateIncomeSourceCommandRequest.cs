using MediatR;

namespace FinanceHelper.Application.Commands.IncomeSources.Create;

public class CreateIncomeSourceCommandRequest : IRequest<CreateIncomeSourceCommandResponse>
{
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string IncomeSourceTypeCode { get; set; } = string.Empty;
    public long OwnerId { get; set; }
}