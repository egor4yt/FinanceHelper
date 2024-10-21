// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace FinanceHelper.Domain.Entities;

public class MetadataType
{
    public string Code { get; set; } = null!;

    public virtual ICollection<MetadataLocalization> MetadataLocalizations { get; set; } = null!;
}