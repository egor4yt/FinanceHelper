namespace FinanceHelper.Domain.Metadata;

public static partial class MetadataLocalization
{
    public static readonly List<Entities.MetadataLocalization> IncomeSourceTypesRu =
    [
        new Entities.MetadataLocalization
        {
            LocalizedValue = "Работа",
            LocalizationKeyword = "Work",
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.IncomeSourceType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Инвестиции",
            LocalizationKeyword = "Investment",
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.IncomeSourceType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Долг",
            LocalizationKeyword = "Debt",
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.IncomeSourceType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Снятие со вклада",
            LocalizationKeyword = "DepositWithdraw",
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.IncomeSourceType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Другое",
            LocalizationKeyword = "Other",
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.IncomeSourceType.Code
        }
    ];
}