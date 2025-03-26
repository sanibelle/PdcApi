using Pdc.Domain.Entities.Common;
using Pdc.Domain.Entities.CourseFramework;
using Pdc.Domain.Entities.Versioning;

namespace Pdc.Domain.Entities.MinisterialSpecification;

public class Competency : Versionable
{
    /// <summary>
    /// Code unique de la compétence. Ex 00SU
    /// </summary>
    public required string Code { get; set; }
    public Units? Units { get; set; } = null;
    public required ProgramOfStudy ProgramOfStudy { get; set; }
    public required bool IsMandatory { get; set; } // true
    public required bool IsOptionnal { get; set; } // true
    public required string StatementOfCompetency { get; set; } // Effectuer le déploiement de serveurs intranet
    public required IList<MinisterialRealisationContext> RealisationContexts { get; set; } // Critères de performance liés à l’ensemble de la compétence
    public required IList<CompetencyElement> CompetencyElements { get; set; }
}
