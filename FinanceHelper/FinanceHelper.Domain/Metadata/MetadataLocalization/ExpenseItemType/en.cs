namespace FinanceHelper.Domain.Metadata;

public static partial class MetadataLocalization
{
    public static readonly List<Entities.MetadataLocalization> ExpenseItemTypesEn =
    [
        new Entities.MetadataLocalization
        {
            LocalizedValue = "Regular expenses",
            LocalizationKeyword = "Expense",
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Investment",
            LocalizationKeyword = "Investment",
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Debt",
            LocalizationKeyword = "Debt",
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Bank deposit",
            LocalizationKeyword = "DepositWithdraw",
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Other",
            LocalizationKeyword = "Other",
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Charity",
            LocalizationKeyword = "Charity",
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.ExpenseItemType.Code
        }
    ];
}