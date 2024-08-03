namespace FinanceHelper.Domain.Entities;

public class TagType
{
    public string Code { get; set; }
    
    public virtual ICollection<Tag> Tags { get; set; }
}