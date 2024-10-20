namespace FinanceHelper.Application.Queries.SupportedLanguages.GetLocalized;

public class GetLocalizedSupportedLanguagesQueryResponse
{
    public List<GetLocalizedSupportedLanguagesQueryResponseItem> Items { get; set; }
}

public class GetLocalizedSupportedLanguagesQueryResponseItem
{
    public string Code { get; set; }
    public string Value { get; set; }
}