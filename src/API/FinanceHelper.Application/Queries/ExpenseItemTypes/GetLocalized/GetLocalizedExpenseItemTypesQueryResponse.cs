namespace FinanceHelper.Application.Queries.ExpenseItemTypes.GetLocalized;

public class GetLocalizedExpenseItemTypesQueryResponse
{
    public List<GetLocalizedExpenseItemTypesQueryResponseItem> Items { get; set; } = null!;
}

public class GetLocalizedExpenseItemTypesQueryResponseItem
{
    public required string Code { get; set; }
    public required string Value { get; set; }
}