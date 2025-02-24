namespace Pdc.Domain.Entities.Version;

public abstract class AVersion
{
    public required Guid Id { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required AVersion NextVersion { get; set; }
    public IList<Change>? Changes { get; set; }
}
