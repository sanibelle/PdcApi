using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Models.MinisterialSpecification;

public class MinisterialCompetencyElement : CompetencyElement
{
    public List<PerformanceCriteria> PerformanceCriterias { get; set; } = new List<PerformanceCriteria>();

    public override void SetVersionOnUnversioned(ChangeRecord version)
    {
        base.SetVersionOnUnversioned(version);
        PerformanceCriterias.ForEach(x => x.SetVersionOnUnversioned(version));
    }
}
