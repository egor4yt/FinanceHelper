namespace FinanceHelper.Domain.Metadata;

public static partial class MetadataLocalization
{
    public static readonly List<Entities.MetadataLocalization> ExpenseItemTypesRu =
    [
        new Entities.MetadataLocalization
        {
            LocalizedValue = "Регулярные траты",
            LocalizationKeyword = ExpenseItemType.Expense.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Инвестиции",
            LocalizationKeyword = ExpenseItemType.Investment.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Долг",
            LocalizationKeyword = ExpenseItemType.Debt.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Другое",
            LocalizationKeyword = ExpenseItemType.Other.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Благотворительность",
            LocalizationKeyword = ExpenseItemType.Charity.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Вклад",
            LocalizationKeyword = ExpenseItemType.Deposit.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.Russian.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        }
    ];
}