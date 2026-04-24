using Pdc.Domain.Interfaces.Versioning;
using Pdc.Domain.Models.Security;

namespace Pdc.Domain.Models.MinisterialSpecification;

public class MinisterialCompetencyElement : CompetencyElement, IChangeablesContainer
{
    public List<PerformanceCriteria> PerformanceCriterias { get; set; } = [];

    public void RemoveDeletedChangeables(List<Guid> changeableIdsToDelete)
    {
        PerformanceCriterias = PerformanceCriterias.Where(x => !changeableIdsToDelete.Contains(x.Id!.Value)).ToList();
    }

    public override void SetChangeRecordOnUntracked(Versioning.ChangeRecord changeRecord)
    {
        if (changeRecord is null) throw new ArgumentNullException(nameof(changeRecord));
        base.SetChangeRecordOnUntracked(changeRecord);
        PerformanceCriterias.ForEach(x => x.SetChangeRecordOnUntracked(changeRecord));
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
