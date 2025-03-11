namespace Pdc.Domain.Entities.Common;

public abstract class Changeable
{
    public required Guid Id { get; set; }
    public required string Description { get; set; }
}
