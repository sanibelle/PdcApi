using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Models.MinisterialSpecification;

public class MinisterialCompetencyElement : CompetencyElement
{
    public List<PerformanceCriteria> PerformanceCriterias { get; set; } = [];

    public override void SetVersionOnUntracked(ChangeRecord version)
    {
        if (version is null) throw new ArgumentNullException(nameof(version));
        base.SetVersionOnUntracked(version);
        PerformanceCriterias.ForEach(x => x.SetVersionOnUntracked(version));
    }

    public override void SetCreatedByOnUntracked(User user)
    {
        base.SetCreatedByOnUntracked(user);
        PerformanceCriterias.ForEach(x => x.SetCreatedByOnUntracked(user));
    }

    public override void SetCreatedOnOnUntracked()
    {
        base.SetCreatedOnOnUntracked();
        PerformanceCriterias.ForEach(x => x.SetCreatedOnOnUntracked());
    }
}
