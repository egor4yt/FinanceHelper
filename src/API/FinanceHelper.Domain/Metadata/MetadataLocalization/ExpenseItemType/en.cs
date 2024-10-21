namespace FinanceHelper.Domain.Metadata;

public static partial class MetadataLocalization
{
    public static readonly List<Entities.MetadataLocalization> ExpenseItemTypesEn =
    [
        new Entities.MetadataLocalization
        {
            LocalizedValue = "Regular expenses",
            LocalizationKeyword = ExpenseItemType.Expense.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Investment",
            LocalizationKeyword = ExpenseItemType.Investment.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Debt",
            LocalizationKeyword = ExpenseItemType.Debt.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Other",
            LocalizationKeyword = ExpenseItemType.Other.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Charity",
            LocalizationKeyword = ExpenseItemType.Charity.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Bank deposit",
            LocalizationKeyword = ExpenseItemType.Deposit.LocalizationKeyword,
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        }
    ];
}