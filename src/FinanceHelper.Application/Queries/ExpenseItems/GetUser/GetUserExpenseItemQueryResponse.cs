namespace FinanceHelper.Application.Queries.ExpenseItems.GetUser;

public class GetUserExpenseItemQueryResponse
{
    public List<GetUserExpenseItemQueryResponseItem> Items { get; set; }
}

public class GetUserExpenseItemQueryResponseItem
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Color { get; set; }
    public GetUserExpenseItemQueryResponseItemTypeDto? ExpenseItemType { get; set; }
    public List<GetUserExpenseItemQueryResponseTagDto> Tags { get; set; }
}

public class GetUserExpenseItemQueryResponseItemTypeDto
{
    public string Code { get; set; }
    public string Name { get; set; }
}

public class GetUserExpenseItemQueryResponseTagDto
{
    public long Id { get; set; }
    public string Name { get; set; }
}