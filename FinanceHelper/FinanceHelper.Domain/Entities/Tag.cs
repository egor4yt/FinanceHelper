namespace FinanceHelper.Domain.Entities;

public class Tag
{
    public long Id { get; set; }
    public long EntityId { get; set; }
    public string TagTypeCode { get; set; }

    public virtual TagType TagType { get; set; }
}