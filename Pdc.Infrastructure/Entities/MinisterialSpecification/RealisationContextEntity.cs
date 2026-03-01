using Pdc.Infrastructure.Entities.Versioning;
using Pdc.Infrastructure.Entities.Visitors;

namespace Pdc.Infrastructure.Entities.MinisterialSpecification;

public class RealisationContextEntity : ChangeableEntity
{
    public virtual CompetencyEntity? Competency { get; set; }
    public override T Accept<T>(IChangeableVisitor<T> visitor) => visitor.Visit(this);
}
