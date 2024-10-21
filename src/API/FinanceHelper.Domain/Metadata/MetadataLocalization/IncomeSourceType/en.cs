namespace FinanceHelper.Domain.Metadata;

public static partial class MetadataLocalization
{
    public static readonly List<Entities.MetadataLocalization> IncomeSourceTypesEn =
    [
        new Entities.MetadataLocalization
        {
            LocalizedValue = "Work",
            LocalizationKeyword = IncomeSourceType.Work.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.IncomeSourceType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Investment",
            LocalizationKeyword = IncomeSourceType.Investment.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.IncomeSourceType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Debt",
            LocalizationKeyword = IncomeSourceType.Debt.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.IncomeSourceType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Bank deposit withdraw",
            LocalizationKeyword = IncomeSourceType.DepositWithdraw.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.IncomeSourceType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Other",
            LocalizationKeyword = IncomeSourceType.Other.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.IncomeSourceType.Code
        }
    ];
}