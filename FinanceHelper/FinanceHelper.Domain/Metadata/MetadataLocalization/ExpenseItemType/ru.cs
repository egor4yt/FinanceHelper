namespace FinanceHelper.Domain.Metadata;

public static partial class MetadataLocalization
{
    public static readonly List<Entities.MetadataLocalization> ExpenseItemTypesRu =
    [
        new Entities.MetadataLocalization
        {
            LocalizedValue = "Регулярные траты",
            LocalizationKeyword = "Expense",
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Инвестиции",
            LocalizationKeyword = "Investment",
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Долг",
            LocalizationKeyword = "Debt",
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Вклад",
            LocalizationKeyword = "DepositWithdraw",
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Другое",
            LocalizationKeyword = "Other",
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Благотворительность",
            LocalizationKeyword = "Charity",
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        }
    ];
}