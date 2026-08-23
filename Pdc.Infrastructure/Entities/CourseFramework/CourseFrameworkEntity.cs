using Pdc.Domain.Models.Common;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using Pdc.Infrastructure.Entities.Version;

namespace Pdc.Infrastructure.Entities.CourseFramework;

public class CourseFrameworkEntity : ChangeRecordableEntity
{
    public Guid? Id { get; set; }
    public virtual IList<CourseFrameworkCompetencyEntity>? CourseFrameworkCompetencies { get; set; } = [];
    public virtual ProgramOfStudyEntity? ProgramOfStudy { get; set; }
    public string? ProgramOfStudyCode { get; set; }
    public virtual IList<CourseFrameworkPerformanceCriteriaEntity>? CourseFrameworkPerformanceCriterias { get; set; } = [];
    public virtual IList<CourseFrameworkEntity>? Prerequisites { get; set; } = [];
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
    public virtual IList<ChangeableEntity>? AssedElements { get; set; } = [];
    public virtual CourseFrameworkChangeableEntity? Name { get; set; }
    public virtual CourseFrameworkChangeableEntity? Code { get; set; }
    public virtual CourseFrameworkChangeableEntity? TheoryHours { get; set; }
    public virtual CourseFrameworkChangeableEntity? LaboratoryHours { get; set; }
    public virtual CourseFrameworkChangeableEntity? PersonnalWorkHours { get; set; }
    public virtual CourseFrameworkChangeableEntity? Semester { get; set; }
    public string FinalCourseObjective { get; set; } = "";
    public string CourseCharacteristics { get; set; } = "";
    /// <summary>
    /// Permet d'ajouter des sections supplémentaires propres aux différents départements sous la forme d'un texte
    /// </summary>
    public string OtherSpecifications { get; set; } = "";
    public string StatementOfComplexAuthenticTask { get; set; } = "";
    public string TaskPresentation { get; set; } = "";
}

