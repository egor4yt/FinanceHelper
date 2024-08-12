namespace FinanceHelper.Domain.Metadata;

public static partial class MetadataLocalization
{
    public static readonly List<Entities.MetadataLocalization> FinancesDistributionItemValueTypesEn =
    [
        new Entities.MetadataLocalization
        {
            LocalizedValue = "Floating",
            LocalizationKeyword = FinancesDistributionItemValueType.Floating.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.FinancesDistributionItemValueType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Fixed divisible",
            LocalizationKeyword = FinancesDistributionItemValueType.Fixed.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.FinancesDistributionItemValueType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Fixed indivisible",
            LocalizationKeyword = FinancesDistributionItemValueType.FixedIndivisible.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.FinancesDistributionItemValueType.Code
        }
    ];
}