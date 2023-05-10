namespace ContactEase.Domain.Common.Entites;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime Created { get; set; }
    public DateTime? LastModified { get; set; }
}
