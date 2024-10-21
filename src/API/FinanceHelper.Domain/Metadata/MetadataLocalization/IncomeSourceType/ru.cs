namespace FinanceHelper.Domain.Metadata;

public static partial class MetadataLocalization
{
    public static readonly List<Entities.MetadataLocalization> IncomeSourceTypesRu =
    [
        new Entities.MetadataLocalization
        {
            LocalizedValue = "Работа",
            LocalizationKeyword = IncomeSourceType.Work.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.IncomeSourceType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Инвестиции",
            LocalizationKeyword = IncomeSourceType.Investment.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.IncomeSourceType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Долг",
            LocalizationKeyword = IncomeSourceType.Debt.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.IncomeSourceType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Снятие со вклада",
            LocalizationKeyword = IncomeSourceType.DepositWithdraw.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.IncomeSourceType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Другое",
            LocalizationKeyword = IncomeSourceType.Other.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.IncomeSourceType.Code
        }
    ];
}