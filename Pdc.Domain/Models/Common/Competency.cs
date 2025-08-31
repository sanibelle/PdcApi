using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Models.MinisterialSpecification;

public class Competency
{
    /// <summary>
    /// Code unique de la compétence. Ex 00SU
    /// </summary>
    public string Code { get; set; } = "";
    public Units? Units { get; set; } = null;
    public string ProgramOfStudyCode { get; set; } = "";
    public bool IsMandatory { get; set; }
    public bool IsOptionnal { get; set; }
    public string StatementOfCompetency { get; set; } = "";// Effectuer le déploiement de serveurs intranet
    public ChangeRecord? CurrentVersion { get; set; }
    public List<RealisationContext> RealisationContexts { get; set; } = new List<RealisationContext>(); // Critères de performance liés à l’ensemble de la compétence

    public virtual void SetVersionOnUnversioned(ChangeRecord version)
    {
        if (CurrentVersion == null)
        {
            CurrentVersion = version;
        }
        RealisationContexts.ForEach(x => x.SetVersionOnUnversioned(version));
    }
}
