using Pdc.Domain.Models.Security;
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

    public override void SetCreatedBy(User createdBy)
    {
        base.SetCreatedBy(createdBy);
        PerformanceCriterias.ForEach(x => x.SetCreatedBy(createdBy));
    }
}
