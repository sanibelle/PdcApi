using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.Security;

namespace Pdc.Domain.Models.MinisterialSpecification;

public class MinisterialCompetency : Competency
{
    public List<MinisterialCompetencyElement> CompetencyElements { get; set; } = [];

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

    public override void RemoveChangeablesByIds(List<Guid> changeableIdsToDelete)
    {
        base.RemoveChangeablesByIds(changeableIdsToDelete);
        CompetencyElements = CompetencyElements.Where(x => !changeableIdsToDelete.Contains(x.Id!.Value)).ToList();
        CompetencyElements.ForEach(x => x.RemoveChangeablesByIds(changeableIdsToDelete));
    }

    public override void SetValueById(Guid id, string value)
    {
        var ce = CompetencyElements.FirstOrDefault(x => x.Id == id);
        CompetencyElements.ForEach(x => x.SetValueById(id, value));
        if (ce is not null)
        {
            ce.Value = value;
            return;
        }
        base.SetValueById(id, value);
    }
}
