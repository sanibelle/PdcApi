using Pdc.Domain.Enums;
using Pdc.Infrastructure.Entities.MinisterialSpecification;

namespace Pdc.Infrastructure.Entities.CourseFramework;

/// <summary>
/// La Value perment d'y inscrire le texte en lien avec le cours.
/// </summary>
public class CourseFrameworkCompetencyElementEntity
{
    public required Guid Id { get; set; } = Guid.NewGuid();
    public required CompetencyElementEntity CompetencyElement { get; set; }
    public required CourseFrameworkEntity CourseFramework { get; set; }
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
