namespace Pdc.Domain.Entities.Versioning;

public class ChangeVersion
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public DateTime CreatedOn { get; set; }
}
