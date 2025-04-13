
using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Models.MinisterialSpecification;

public class MinisterialCompetency : Competency
{
    public List<MinisterialCompetencyElement> CompetencyElements { get; set; } = new List<MinisterialCompetencyElement>();

    public void SetCreatedBy(User createdBy)
    {
        RealisationContexts.ForEach(x => x.SetCreatedBy(createdBy));
        CompetencyElements.ForEach(x => x.SetCreatedBy(createdBy));
    }

    public override void SetVersion(ChangeRecord version)
    {
        base.SetVersion(version);
        CurrentVersion = version;
        CompetencyElements.ForEach(x => x.SetVersion(version));
        RealisationContexts.ForEach(x => x.SetVersion(version));
    }
}
