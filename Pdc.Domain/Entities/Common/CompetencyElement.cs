using Pdc.Domain.Entities.Versioning;

namespace Pdc.Domain.Entities.Common;

public class CompetencyElement : AChangeable
{
    public required int Position { get; set; }

    public required IEnumerable<PerformanceCriteria> PerformanceCriterias { get; set; }
}
