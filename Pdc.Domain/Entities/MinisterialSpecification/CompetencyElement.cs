using Pdc.Domain.Entities.Versioning;

namespace Pdc.Domain.Entities.MinisterialSpecification;

public class CompetencyElement : Changeable
{
    public required int Position { get; set; }

    public required IEnumerable<PerformanceCriteria> PerformanceCriterias { get; set; }
}
