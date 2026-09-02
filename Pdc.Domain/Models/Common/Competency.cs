using Pdc.Domain.Interfaces.Propagables;
using Pdc.Domain.Interfaces.Versioning;
using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Models.Common;

public class Competency : AChangeRecordable, ICreatedByPropagable, ICreatedOnPropagable, IChangeablesContainer
{
    /// <summary>
    /// Code unique de la compétence. Ex 00SU
    /// </summary>
    public string Code { get; set; } = "";
    public Guid? UnitsID { get; set; } = null;
    public Units? Units { get; set; } = null;
    public string ProgramOfStudyCode { get; set; } = "";
    public bool IsMandatory { get; set; }
    public bool IsOptional { get; set; }
    public string StatementOfCompetency { get; set; } = "";// Effectuer le déploiement de serveurs intranet
    public List<RealisationContext> RealisationContexts { get; set; } = new List<RealisationContext>(); // Critères de performance liés à l’ensemble de la compétence

    public override void SetChangeRecordOnUntracked(ChangeRecord changeRecord)
    {
        if (ChangeRecord == null)
        {
            ChangeRecord = changeRecord;
        }
        RealisationContexts.ForEach(x => x.SetChangeRecordOnUntracked(changeRecord));
    }

    public virtual void SetCreatedByOnUntracked(User user)
    {
        RealisationContexts.ForEach(x => x.SetCreatedByOnUntracked(user));
    }

    public virtual void SetCreatedOnOnUntracked()
    {
        RealisationContexts.ForEach(x => x.SetCreatedOnOnUntracked());
    }

    public virtual void RemoveChangeablesByIds(List<Guid> changeableIdsToDelete)
    {
        RealisationContexts = RealisationContexts.Where(x => !changeableIdsToDelete.Contains(x.Id!.Value)).ToList();
    }

    public virtual void SetValueById(Guid id, string value)
    {
        var rc = RealisationContexts.FirstOrDefault(x => x.Id == id);
        if (rc is not null)
        {
            rc.Value = value;
        }
    }
}
