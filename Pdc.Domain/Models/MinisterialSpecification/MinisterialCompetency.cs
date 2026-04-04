using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.Security;

namespace Pdc.Domain.Models.MinisterialSpecification;

public class MinisterialCompetency : Competency
{
    public List<MinisterialCompetencyElement> CompetencyElements { get; set; } = new List<MinisterialCompetencyElement>();

    public bool IsDraftAndV1OrNull()
    {
        if (ChangeRecord == null)
        {
            return true;
        }
        return ChangeRecord.IsDraft && ChangeRecord.ChangeRecordNumber == 1;
    }

    public override void SetCreatedByOnUntracked(User user)
    {
        base.SetCreatedByOnUntracked(user);
        CompetencyElements.ForEach(x => x.SetCreatedByOnUntracked(user));
    }

    public override void SetChangeRecordOnUntracked(Versioning.ChangeRecord changeRecord)
    {
        base.SetChangeRecordOnUntracked(changeRecord);
        CompetencyElements.ForEach(x => x.SetChangeRecordOnUntracked(changeRecord));
    }

    public override void SetCreatedOnOnUntracked()
    {
        base.SetCreatedOnOnUntracked();
        CompetencyElements.ForEach(x => x.SetCreatedOnOnUntracked());
    }
}
