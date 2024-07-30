namespace FinanceHelper.Domain.Entities;

public class MetadataLocalization
{
    public string SupportedLanguageCode { get; set; }
    public string LocalizedValue { get; set; }
    public string LocalizationKeyword { get; set; }
    public string MetadataTypeCode { get; set; }

    public virtual SupportedLanguage SupportedLanguage { get; set; }
    public virtual MetadataType MetadataType { get; set; }
}