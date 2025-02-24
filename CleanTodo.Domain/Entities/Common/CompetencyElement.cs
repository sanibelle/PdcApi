namespace Pdc.Domain.Entities.Common;

public abstract class CompetencyElement : Changeable
{
    public required int Position { get; set; }
    public required IEnumerable<PerformanceCriteria> PerformanceCriterias { get; set; }
}
