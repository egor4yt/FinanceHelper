// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace FinanceHelper.Domain.Entities;

public class MetadataLocalization
{
    public string SupportedLanguageCode { get; set; } = null!;
    public string LocalizedValue { get; set; } = null!;
    public string LocalizationKeyword { get; set; } = null!;
    public string MetadataTypeCode { get; set; } = null!;

    public virtual SupportedLanguage SupportedLanguage { get; set; } = null!;
    public virtual MetadataType MetadataType { get; set; } = null!;
}