namespace FinanceHelper.Application.Queries.IncomeSourceTypes.GetLocalized;

public class GetLocalizedIncomeSourceTypesQueryResponse
{
    public List<GetLocalizedIncomeSourceTypesQueryResponseItem> Items { get; set; } = null!;
}

public class GetLocalizedIncomeSourceTypesQueryResponseItem
{
    public required string Code { get; set; }
    public required string Value { get; set; }
}