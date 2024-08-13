namespace FinanceHelper.Application.Queries.IncomeSources.GetUser;

public class GetUserIncomeSourceQueryResponse
{
    public List<GetUserIncomeSourceQueryResponseItem> Items { get; set; }
}

public class GetUserIncomeSourceQueryResponseItem
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Color { get; set; }
    public GetUserIncomeSourceQueryResponseItemTypeDto IncomeSourceType { get; set; }
}

public class GetUserIncomeSourceQueryResponseItemTypeDto
{
    public string Code { get; set; }
    public string Name { get; set; }
}