using Pdc.Infrastructure.Entities.Visitors;

namespace Pdc.Infrastructure.Entities.Versioning;

public abstract class ChangeableEntity
{
    public Guid? Id { get; set; }
    public required string Value { get; set; }
    public required ICollection<ComplementaryInformationEntity> ComplementaryInformations { get; set; }
    public abstract T Accept<T>(IChangeableVisitor<T> visitor);
}
