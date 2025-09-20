using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Models.MinisterialSpecification;

public class MinisterialCompetencyElement : CompetencyElement
{
    public List<PerformanceCriteria> PerformanceCriterias { get; set; } = [];

    public override void SetVersionOnUnversioned(ChangeRecord version)
    {
        if (version is null) throw new ArgumentNullException(nameof(version));
        base.SetVersionOnUnversioned(version);
        PerformanceCriterias.ForEach(x => x.SetVersionOnUnversioned(version));
    }
}
