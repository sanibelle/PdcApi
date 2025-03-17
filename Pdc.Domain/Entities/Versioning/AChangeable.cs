namespace Pdc.Domain.Entities.Versioning;

public abstract class AChangeable
{
    public required Guid Id { get; set; }
    public required string Description { get; set; }
}
