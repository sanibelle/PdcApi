using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.Version;

namespace Pdc.Infrastructure.Entities.MinisterialSpecification;

public class CompetencyEntity : ChangeRecordableEntity
{
    /// <summary>
    /// Code unique de la compétence. Ex 00SU
    /// </summary>
    public required string Code { get; set; }
    public virtual UnitsEntity? Units { get; set; }
    public Guid? UnitsId { get; set; }
    public virtual ProgramOfStudyEntity? ProgramOfStudy { get; set; }
    public string? ProgramOfStudyCode { get; set; }
    public bool IsMandatory { get; set; }
    public bool IsOptional { get; set; }
    public string StatementOfCompetency { get; set; } = "";
    public virtual ICollection<RealisationContextEntity> RealisationContexts { get; set; } = new List<RealisationContextEntity>();
    public virtual ICollection<CompetencyElementEntity> CompetencyElements { get; set; } = new List<CompetencyElementEntity>();
}
