namespace Pdc.Infrastructure.Entities.Versioning;

public class ChangeableEntity
{
    public Guid? Id { get; set; }
    public required string Value { get; set; }
    public required ICollection<ComplementaryInformationEntity> ComplementaryInformations { get; set; }
}
