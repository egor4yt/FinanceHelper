namespace FinanceHelper.Application.Queries.ExpenseItems.GetUser;

public class GetUserExpenseItemQueryResponse
{
    public List<GetUserExpenseItemQueryResponseItem> Items { get; set; }
}

public class GetUserExpenseItemQueryResponseItem
{
    public long ExpenseItemId { get; set; }
    public string Name { get; set; }
    public string? Color { get; set; }
    public GetUserExpenseItemQueryResponseItemTypeDto? ExpenseItemType { get; set; }
}

public class GetUserExpenseItemQueryResponseItemTypeDto
{
    public string Name { get; set; }
    public string TypeCode { get; set; }
}