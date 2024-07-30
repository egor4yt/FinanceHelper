namespace FinanceHelper.Domain.Entities;

public class MetadataType
{
    public string Code { get; set; }

    public virtual ICollection<MetadataLocalization> MetadataLocalizations { get; set; }
}