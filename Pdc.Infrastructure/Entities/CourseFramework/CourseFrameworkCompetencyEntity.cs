using Pdc.Infrastructure.Entities.MinisterialSpecification;

namespace Pdc.Infrastructure.Entities.CourseFramework;

/// <summary>
/// La Value perment d'y inscrire le texte en lien avec le cours.
/// </summary>
public class CourseFrameworkCompetencyEntity
{
    public Guid? Id { get; set; }
    public virtual CompetencyEntity? Competency { get; set; }
    public virtual CourseFrameworkEntity? CourseFramework { get; set; }
    public int? Hours { get; set; }
    /// <summary>
    /// Requise si la compétence est étalée sur plusieurs cours
    /// </summary>
    public string? CompetencyDistribution { get; set; }
    public required string CompetencyCode { get; set; }
    public required Guid CourseFrameworkId { get; set; }
}
