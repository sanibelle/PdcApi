using Pdc.Domain.Entities.Versioning;

namespace Pdc.Domain.Entities.MinisterialSpecification;

public class CompetencyElement : Changeable
{
    private IEnumerable<PerformanceCriteria> _performanceCriterias { get; set; } = new List<PerformanceCriteria>();
    public required int Position { get; set; }

}
