namespace Checkout.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public string? LastModifiedBy { get; set; }
    public DateTime LastModifiedAt { get; set; }

    public void SetCreatedAudit(string createdBy)
    {
        CreatedBy = createdBy;
        CreatedAt = DateTime.UtcNow;
    }

    public void SetLastModifiedAudit(string modifiedBy)
    {
        LastModifiedBy = modifiedBy;
        LastModifiedAt = DateTime.UtcNow;
    }
}