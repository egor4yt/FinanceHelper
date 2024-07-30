namespace FinanceHelper.Domain.Metadata;

public partial class MetadataLocalization
{
    public static List<Entities.MetadataLocalization> FinancesDistributionItemValueTypesEn = new List<Entities.MetadataLocalization>
    {
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
    };
}