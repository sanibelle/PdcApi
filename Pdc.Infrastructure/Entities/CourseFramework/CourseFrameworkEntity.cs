using Pdc.Domain.Entities.Common;
using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Infrastructure.Entities.CourseFramework;

public class CourseFrameworkEntity : VersionableEntity
{
    public IEnumerable<CourseFrameworkCompetencyEntity> CourseFrameworkCompetencies { get; set; } = new List<CourseFrameworkCompetencyEntity>();
    public IEnumerable<CourseFrameworkPerformanceCriteriaEntity> CourseFrameworkPerformanceCriterias { get; set; } = new List<CourseFrameworkPerformanceCriteriaEntity>();
    public IEnumerable<CourseFrameworkEntity> Prerequisites { get; set; } = new List<CourseFrameworkEntity>();
    /// <summary>
    /// Éléments évalués. Peut être un : 
    /// <list type="bullet">
    ///    <item>
    ///        <term>Un élément de compétence</term>
    ///        <definition><see cref="CompetencyElementEntity"/></definition>
    ///    </item>
    ///    <item>
    ///        <term>Un critère de performance</term>
    ///        <definition><see cref="PerformanceCriteriaEntity"/></definition>
    ///    </item>
    ///    <item>
    ///        <term>Un élément de contenu</term>
    ///        <definition><see cref="ContentElement"/></definition>
    ///    </item>
    ///    <item>
    ///        <term>Du texte</term>
    ///        <definition><see cref="ChangeableEntity"/></definition>
    ///    </item>
    ///</list>
    /// </summary>
    public IEnumerable<ChangeableEntity> AssedElements { get; set; } = new List<ChangeableEntity>();
    public required string Name { get; set; }
    public required string CourseCode { get; set; }
    public required Weighting Weighting { get; set; }
    public required int Semester { get; set; }
    /// <summary>
    /// Durée du cours en heures
    /// </summary>
    public required int Hours { get; set; }
    public required Units Units { get; set; }
    public required string FinalCourseObjective { get; set; }
    public required string CourseCharacteristics { get; set; }
    /// <summary>
    /// Permet d'ajouter des sections supplémentaires propres aux différents départements sous la forme d'un texte
    /// </summary>
    public string? OtherSpecifications { get; set; }
    // TODO inférer le niveau atteint de taxonomie de Bloom
    public required string StatementOfComplexAuthenticTask { get; set; }
    public required string TaskPresentation { get; set; }
}

