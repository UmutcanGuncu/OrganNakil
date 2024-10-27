namespace OrganNakil.Domain.Common;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
}