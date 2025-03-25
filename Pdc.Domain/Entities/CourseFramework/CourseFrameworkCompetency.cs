using Pdc.Domain.Entities.MinisterialSpecification;
using Pdc.Domain.Enums;

namespace Pdc.Domain.Entities.CourseFramework;

/// <summary>
/// La Value perment d'y inscrire le texte en lien avec le cours.
/// </summary>
public class CourseFrameworkCompetency
{
    public required Competency Competency { get; set; }
    public required CourseFramework CourseFramework { get; set; }
    public required int Hours { get; set; }
    public bool IsTerminalyEvaluated
    {
        get
        {
            return ReachedLevel == BloomTaxonomy.Creating;
        }
    }
    public BloomTaxonomy ReachedLevel { get; set; }
    /// <summary>
    /// Requise si la compétence est étalée sur plusieurs cours
    /// </summary>
    public string? CompetencyDistribution { get; set; }
    //TODO ajouter une logique dans le code pour aller chercher le nombre d'heure totale de la compétence
}
