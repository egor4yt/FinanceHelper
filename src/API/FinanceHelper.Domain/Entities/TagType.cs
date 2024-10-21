// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace FinanceHelper.Domain.Entities;

public class TagType
{
    public string Code { get; set; } = null!;

    public virtual ICollection<Tag> Tags { get; set; } = null!;
}