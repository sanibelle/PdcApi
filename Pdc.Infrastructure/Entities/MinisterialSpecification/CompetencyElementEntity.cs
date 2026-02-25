using Pdc.Infrastructure.Entities.Versioning;
using Pdc.Infrastructure.Entities.Visitors;

namespace Pdc.Infrastructure.Entities.MinisterialSpecification;

public class CompetencyElementEntity : ChangeableEntity
{
    public ICollection<PerformanceCriteriaEntity> PerformanceCriterias { get; set; } = new List<PerformanceCriteriaEntity>();
    public required int Position { get; set; }
    public virtual CompetencyEntity? Competency { get; set; }

    public override T Accept<T>(IChangeableVisitor<T> visitor) => visitor.Visit(this);
}
