﻿namespace FinanceHelper.Domain.Metadata;

public static partial class MetadataLocalization
{
    public static readonly List<Entities.MetadataLocalization> IncomeSourceTypesEn =
    [
        new Entities.MetadataLocalization
        {
            LocalizedValue = "Work",
            LocalizationKeyword = "Work",
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.IncomeSourceType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Investment",
            LocalizationKeyword = "Investment",
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.IncomeSourceType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Debt",
            LocalizationKeyword = "Debt",
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.IncomeSourceType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Bank deposit withdraw",
            LocalizationKeyword = "DepositWithdraw",
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.IncomeSourceType.Code
        },

        new Entities.MetadataLocalization
        {
            LocalizedValue = "Other",
            LocalizationKeyword = "Other",
            SupportedLanguageCode = SupportedLanguage.English.Code,
            MetadataTypeCode = MetadataType.IncomeSourceType.Code
        }
    ];
}