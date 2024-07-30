namespace FinanceHelper.Domain.Metadata;

public partial class MetadataLocalization
{
    public static readonly List<Entities.MetadataLocalization> FinancesDistributionItemValueTypesRu =
    [
        new Entities.MetadataLocalization
        {
            LocalizedValue = "Процентный",
            LocalizationKeyword = "Floating",
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.FinancesDistributionItemValueType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Фиксированный",
            LocalizationKeyword = "Fixed",
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.FinancesDistributionItemValueType.Code
        }
    ];
}