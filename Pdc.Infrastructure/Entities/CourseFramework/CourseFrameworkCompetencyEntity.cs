using Pdc.Infrastructure.Entities.MinisterialSpecification;

namespace Pdc.Infrastructure.Entities.CourseFramework;

/// <summary>
/// La Value perment d'y inscrire le texte en lien avec le cours.
/// </summary>
public class CourseFrameworkCompetencyEntity
{
    public required Guid? Id { get; set; }
    public required CompetencyEntity Competency { get; set; }
    public required CourseFrameworkEntity CourseFramework { get; set; }
    public required int Hours { get; set; }
    /// <summary>
    /// Requise si la compétence est étalée sur plusieurs cours
    /// </summary>
    public string? CompetencyDistribution { get; set; }
    public bool IsAssedElement { get; set; } = false;

}
