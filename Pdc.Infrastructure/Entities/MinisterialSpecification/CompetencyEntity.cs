using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Infrastructure.Entities.MinisterialSpecification;

public class CompetencyEntity : VersionableEntity
{
    /// <summary>
    /// Code unique de la compétence. Ex 00SU
    /// </summary>
    public required string Code { get; set; }
    public virtual UnitsEntity? Units { get; set; }
    public virtual Guid? UnitsId { get; set; }
    public virtual ProgramOfStudyEntity? ProgramOfStudy { get; set; }
    public bool IsMandatory { get; set; } // true
    public bool IsOptional { get; set; } // true
    public string StatementOfCompetency { get; set; } = ""; // Effectuer le déploiement de serveurs intranet
    public virtual ICollection<RealisationContextEntity> RealisationContexts { get; set; } = new List<RealisationContextEntity>();
    public virtual ICollection<CompetencyElementEntity> CompetencyElements { get; set; } = new List<CompetencyElementEntity>();
}
