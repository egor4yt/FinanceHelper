namespace FinanceHelper.Domain.Common;

public interface ISoftDeletable
{
    public DateTime? DeletedAt { get; set; }
}