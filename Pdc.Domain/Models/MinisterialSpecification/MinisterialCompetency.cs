
using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Models.MinisterialSpecification;

public class MinisterialCompetency : Competency
{
    public List<MinisterialCompetencyElement> CompetencyElements { get; set; } = new List<MinisterialCompetencyElement>();

    public override void SetVersion(ChangeRecord version)
    {
        base.SetVersion(version);
        CurrentVersion = version;
        CompetencyElements.ForEach(x => x.SetVersion(version));
        RealisationContexts.ForEach(x => x.SetVersion(version));
    }
}
