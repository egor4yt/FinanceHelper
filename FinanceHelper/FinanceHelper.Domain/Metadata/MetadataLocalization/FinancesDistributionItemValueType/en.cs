namespace FinanceHelper.Domain.Metadata;

public partial class MetadataLocalization
{
    public static readonly List<Entities.MetadataLocalization> FinancesDistributionItemValueTypesEn =
    [
        new Entities.MetadataLocalization
        {
            LocalizedValue = "Floating",
            LocalizationKeyword = "Floating",
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.FinancesDistributionItemValueType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Fixed",
            LocalizationKeyword = "Fixed",
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.FinancesDistributionItemValueType.Code
        }
    ];
}