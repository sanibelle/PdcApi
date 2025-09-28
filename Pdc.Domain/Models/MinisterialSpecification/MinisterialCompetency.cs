using Pdc.Domain.Models.Security;
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

    public void SetCreatedByOnUntracked(User user)
    {
        base.SetCreatedByOnUntracked(user);
        CompetencyElements.ForEach(x => x.SetCreatedByOnUntracked(user));
    }

    public override void SetVersionOnUntracked(ChangeRecord version)
    {
        base.SetVersionOnUntracked(version);
        CompetencyElements.ForEach(x => x.SetVersionOnUntracked(version));
    }
}
