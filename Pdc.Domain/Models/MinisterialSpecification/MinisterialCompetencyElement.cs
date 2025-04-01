namespace Pdc.Domain.Models.MinisterialSpecification;

public class MinisterialCompetencyElement : CompetencyElement
{
    public IEnumerable<PerformanceCriteria> PerformanceCriterias { get; set; } = new List<PerformanceCriteria>();
}
