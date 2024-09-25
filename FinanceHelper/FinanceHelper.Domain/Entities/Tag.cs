// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace FinanceHelper.Domain.Entities;

public class Tag
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string TagTypeCode { get; set; } = null!;
    public long OwnerId { get; set; }

    public virtual TagType TagType { get; set; } = null!;
    public virtual User Owner { get; set; } = null!;
}