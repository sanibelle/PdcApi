using Pdc.Domain.Enums;
using Pdc.Infrastructure.Entities.MinisterialSpecification;

namespace Pdc.Infrastructure.Entities.CourseFramework;

/// <summary>
/// La Value perment d'y inscrire le texte en lien avec le cours.
/// </summary>
public class CourseFrameworkCompetencyEntity
{
    public required Guid Id { get; set; } = Guid.NewGuid();
    public required CompetencyEntity Competency { get; set; }
    public required CourseFrameworkEntity CourseFramework { get; set; }
    public required int Hours { get; set; }
    public bool IsTerminalyEvaluated
    {
        get
        {
            return ReachedTaxonomyLevel == BloomTaxonomy.Creating;
        }
    }
    // TODO inférer le niveau d'enseignement en fonction des niveaux atteints par les éléments de compétences
    public BloomTaxonomy ReachedTaxonomyLevel { get; set; }
    /// <summary>
    /// Requise si la compétence est étalée sur plusieurs cours
    /// </summary>
    public string? CompetencyDistribution { get; set; }
    //TODO ajouter une logique dans le code pour aller chercher le nombre d'heure totale de la compétence
}
