namespace FinanceHelper.Domain.Entities;

public class SupportedLanguage
{
    public string Code { get; set; }
    public string LocalizedValue { get; set; }

    public virtual ICollection<MetadataLocalization> MetadataLocalizations { get; set; }
}