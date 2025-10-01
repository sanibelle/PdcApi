using Pdc.Domain.Models.Common;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Infrastructure.Entities.MinisterialSpecification;

public class CompetencyEntity : VersionableEntity
{
    /// <summary>
    /// Code unique de la compétence. Ex 00SU
    /// </summary>
    public required string Code { get; set; }
    public Units? Units { get; set; } = null;
    public required ProgramOfStudyEntity ProgramOfStudy { get; set; }
    public bool IsMandatory { get; set; } // true
    public bool IsOptional { get; set; } // true
    public string StatementOfCompetency { get; set; } = ""; // Effectuer le déploiement de serveurs intranet
    public ICollection<RealisationContextEntity> RealisationContexts { get; set; } = new List<RealisationContextEntity>(); // Critères de performance liés à l’ensemble de la compétence
    public ICollection<CompetencyElementEntity> CompetencyElements { get; set; } = new List<CompetencyElementEntity>();

    public CompetencyEntity()
    {
    }
}
