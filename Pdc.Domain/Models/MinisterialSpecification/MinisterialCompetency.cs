using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Models.MinisterialSpecification;

public class MinisterialCompetency : Competency
{
    public List<MinisterialCompetencyElement> CompetencyElements { get; set; } = new List<MinisterialCompetencyElement>();

    public bool IsDraftAndV1OrNull()
    {
        if (CurrentVersion == null)
        {
            return true;
        }
        return CurrentVersion.IsDraft && CurrentVersion.VersionNumber == 1;
    }

    public override void SetVersionOnUnversioned(ChangeRecord version)
    {
        base.SetVersionOnUnversioned(version);
        CompetencyElements.ForEach(x => x.SetVersionOnUnversioned(version));
    }
}
