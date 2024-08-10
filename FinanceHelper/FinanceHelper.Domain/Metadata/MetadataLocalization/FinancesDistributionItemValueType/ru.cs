namespace FinanceHelper.Domain.Metadata;

public static partial class MetadataLocalization
{
    public static readonly List<Entities.MetadataLocalization> FinancesDistributionItemValueTypesRu =
    [
        new Entities.MetadataLocalization
        {
            LocalizedValue = "Процентный",
            LocalizationKeyword = FinancesDistributionItemValueType.Floating.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.FinancesDistributionItemValueType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Фиксированный",
            LocalizationKeyword = FinancesDistributionItemValueType.Fixed.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.FinancesDistributionItemValueType.Code
        }
    ];
}