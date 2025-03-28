using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Infrastructure.Entities.MinisterialSpecification;

public class CompetencyElementEntity : ChangeableEntity
{
    public IEnumerable<PerformanceCriteriaEntity> PerformanceCriterias { get; set; } = new List<PerformanceCriteriaEntity>();
    public required int Position { get; set; }

}
