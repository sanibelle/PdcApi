using Pdc.Domain.Entities.MinisterialSpecification;
using Pdc.Domain.Enums;

namespace Pdc.Domain.Entities.CourseFramework;

/// <summary>
/// La Value perment d'y inscrire le texte en lien avec le cours.
/// </summary>
public class CourseFrameworkCompetencyElement
{
    public required Guid Id { get; set; } = Guid.NewGuid();
    public required CompetencyElement CompetencyElement { get; set; }
    public required CourseFramework CourseFramework { get; set; }
    public required int Hours { get; set; }
    public bool IsTerminalyEvaluated
    {
        get
        {
            return ReachedTaxonomyLevel == BloomTaxonomy.Creating;
        }
    }
    public BloomTaxonomy ReachedTaxonomyLevel { get; set; }
    public TeachedLevelType TeachedLevel { get; set; }
    public bool IsAssedElement { get; set; } = false;
}
