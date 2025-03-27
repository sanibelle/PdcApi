using Pdc.Domain.Entities.Common;
using Pdc.Domain.Entities.CourseFramework;

namespace Pdc.Domain.Entities.MinisterialSpecification;

public class Competency
{
    /// <summary>
    /// Code unique de la compétence. Ex 00SU
    /// </summary>
    public string Code { get; set; }
    public int VersionNumber { get; set; }
    public Units? Units { get; set; } = null;
    public ProgramOfStudy ProgramOfStudy { get; set; }
    public bool IsMandatory { get; set; } // true
    public bool IsOptionnal { get; set; } // true
    public string StatementOfCompetency { get; set; } // Effectuer le déploiement de serveurs intranet
    public IList<MinisterialRealisationContext> RealisationContexts { get; set; } // Critères de performance liés à l’ensemble de la compétence
    public IList<CompetencyElement> CompetencyElements { get; set; }
}
