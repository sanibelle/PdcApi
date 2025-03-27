using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Infrastructure.Entities.MinisterialSpecification;

public class CompetencyElementEntity : ChangeableEntity
{
    private IEnumerable<PerformanceCriteriaEntity> _performanceCriterias { get; set; } = new List<PerformanceCriteriaEntity>();
    public required int Position { get; set; }

}
