namespace Pdc.Domain.Entities.MinisterialSpecification;

public class MinisterialCompetencyElement : CompetencyElement
{
    public IEnumerable<PerformanceCriteria> PerformanceCriterias { get; set; } = new List<PerformanceCriteria>();
}
