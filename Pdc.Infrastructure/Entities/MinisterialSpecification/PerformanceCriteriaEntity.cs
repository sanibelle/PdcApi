using Pdc.Infrastructure.Entities.Versioning;
using Pdc.Infrastructure.Entities.Visitors;

namespace Pdc.Infrastructure.Entities.MinisterialSpecification;

public class PerformanceCriteriaEntity : ChangeableEntity
{
    public required int Position { get; set; }
    public CompetencyElementEntity? CompetencyElement { get; set; }
    public override T Accept<T>(IChangeableVisitor<T> visitor) => visitor.Visit(this);
}
