using Pdc.Domain.Entities.Common;

namespace Pdc.Domain.Entities.MinisterialSpecification;

public class Competency
{
    /// <summary>
    /// Code unique de la compétence. Ex 00SU
    /// </summary>
    public string Code { get; set; } = "";
    public int VersionNumber { get; set; }
    public Units? Units { get; set; } = null;
    public string ProgramOfStudyCode { get; set; } = "";
    public bool IsMandatory { get; set; }
    public bool IsOptionnal { get; set; }
    public string StatementOfCompetency { get; set; } = "";// Effectuer le déploiement de serveurs intranet
    public IEnumerable<RealisationContext> RealisationContexts { get; set; } = new List<RealisationContext>(); // Critères de performance liés à l’ensemble de la compétence
}
