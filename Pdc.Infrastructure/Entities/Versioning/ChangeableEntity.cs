using Pdc.Infrastructure.Entities.Visitors;

namespace Pdc.Infrastructure.Entities.Versioning;

public abstract class ChangeableEntity
{
    public Guid? Id { get; set; }
    public required string Value { get; set; }
    public virtual ICollection<ComplementaryInformationEntity> ComplementaryInformations { get; set; } = new List<ComplementaryInformationEntity>();
    public abstract T Accept<T>(IChangeableVisitor<T> visitor);
}
