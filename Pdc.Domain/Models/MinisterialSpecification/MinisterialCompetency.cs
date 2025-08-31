using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Models.MinisterialSpecification;

public class MinisterialCompetency : Competency
{
    public List<MinisterialCompetencyElement> CompetencyElements { get; set; } = new List<MinisterialCompetencyElement>();

    public override void SetVersionOnUnversioned(ChangeRecord version)
    {
        base.SetVersionOnUnversioned(version);
        if (CurrentVersion == null)
        {
            CurrentVersion = version;
        }
        CompetencyElements.ForEach(x => x.SetVersionOnUnversioned(version));
        RealisationContexts.ForEach(x => x.SetVersionOnUnversioned(version));
    }
}
