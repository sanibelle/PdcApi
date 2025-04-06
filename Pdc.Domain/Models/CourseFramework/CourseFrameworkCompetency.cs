using Pdc.Domain.Enums;

namespace Pdc.Domain.Models.MinisterialSpecification;

public class CourseFrameworkCompetency : MinisterialCompetencyEntity
{
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
