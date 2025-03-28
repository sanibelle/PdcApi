namespace Pdc.Infrastructure.Entities.Versioning;

public class ChangeableEntity
{
    public required Guid Id { get; set; }
    public required string Value { get; set; }
    public required IEnumerable<ComplementaryInformationEntity> ComplementaryInformations { get; set; }
}
