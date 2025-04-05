using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Models.MinisterialSpecification;

public class MinisterialCompetencyElement : CompetencyElement
{
    public List<PerformanceCriteriaDTO> PerformanceCriterias { get; set; } = new List<PerformanceCriteriaDTO>();

    public void SetVersion(ChangeRecord version)
    {
        base.SetVersion(version);
        PerformanceCriterias.ForEach(x => x.SetVersion(version));
    }
}
