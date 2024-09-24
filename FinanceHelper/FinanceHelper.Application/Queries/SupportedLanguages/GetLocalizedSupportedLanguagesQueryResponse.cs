namespace FinanceHelper.Application.Queries.SupportedLanguages;

public class GetLocalizedSupportedLanguagesQueryResponse 
{
    public List<GetLocalizedSupportedLanguagesQueryResponseItem> Items { get; set; }
}

public class GetLocalizedSupportedLanguagesQueryResponseItem
{
    public string Code { get; set; }
    public string Value { get; set; }
}