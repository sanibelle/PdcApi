using Pdc.Domain.Interfaces.Propagables;
using Pdc.Domain.Models.Security;

namespace Pdc.Domain.Models.Common;

public class Competency : IChangeRecordPropagable, ICreatedByPropagable, ICreatedOnPropagable
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
    public Versioning.ChangeRecord? ChangeRecord { get; set; }
    public List<RealisationContext> RealisationContexts { get; set; } = new List<RealisationContext>(); // Critères de performance liés à l’ensemble de la compétence

    public virtual void SetChangeRecordOnUntracked(Versioning.ChangeRecord changeRecord)
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
}
