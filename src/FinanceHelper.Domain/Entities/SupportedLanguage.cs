// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace FinanceHelper.Domain.Entities;

public class SupportedLanguage
{
    public string Code { get; set; } = null!;
    public string LocalizedValue { get; set; } = null!;

    public virtual ICollection<MetadataLocalization> MetadataLocalizations { get; set; } = null!;
    public virtual ICollection<User> Users { get; set; } = null!;
}