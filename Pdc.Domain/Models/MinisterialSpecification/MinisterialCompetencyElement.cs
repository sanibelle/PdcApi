using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Models.MinisterialSpecification;

public class MinisterialCompetencyElement : CompetencyElement
{
    public List<PerformanceCriteria> PerformanceCriterias { get; set; } = new List<PerformanceCriteria>();

    public override void SetVersion(ChangeRecord version)
    {
        base.SetVersion(version);
        PerformanceCriterias.ForEach(x => x.SetVersion(version));
    }
}
