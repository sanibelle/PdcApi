using Pdc.Domain.Entities.Common;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Infrastructure.Entities.MinisterialSpecification;

public class CompetencyEntity : VersionableEntity
{
    /// <summary>
    /// Code unique de la compétence. Ex 00SU
    /// </summary>
    public string Code { get; set; }
    public Units? Units { get; set; } = null;
    public ProgramOfStudyEntity ProgramOfStudy { get; set; }
    public bool IsMandatory { get; set; } // true
    public bool IsOptionnal { get; set; } // true
    public string StatementOfCompetency { get; set; } // Effectuer le déploiement de serveurs intranet
    public IEnumerable<RealisationContextEntity> RealisationContexts { get; set; } // Critères de performance liés à l’ensemble de la compétence
    public IEnumerable<CompetencyElementEntity> CompetencyElements { get; set; }

    public CompetencyEntity()
    {

    }
}
