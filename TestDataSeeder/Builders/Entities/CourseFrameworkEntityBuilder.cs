using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.Version;

namespace TestDataSeeder.Builders.Entities;

public class CourseFrameworkEntityBuilder(ProgramOfStudyEntity programOfStudy)
{
    private ProgramOfStudyEntity _programOfStudy = programOfStudy;
    private ChangeRecordEntity? _changeRecord;
    public Guid? _id { get; set; }
    // TODO public virtual IList<CourseFrameworkCompetencyEntity>? CourseFrameworkCompetencies { get; set; }
    // TODO public virtual IList<CourseFrameworkPerformanceCriteriaEntity>? CourseFrameworkPerformanceCriterias { get; set; }
    // TODO public virtual IList<CourseFrameworkEntity>? Prerequisites { get; set; }
    // TODO public virtual IList<ChangeableEntity>? AssedElements { get; set; } = new List<ChangeableEntity>();
    public CourseFrameworkChangeableEntity _name { get; set; } = new CourseFrameworkChangeableEntityBuilder().WithValue("Seeder default name").Build();
    public CourseFrameworkChangeableEntity _code { get; set; } = new CourseFrameworkChangeableEntityBuilder().WithValue("11-111-11").Build();
    public CourseFrameworkChangeableEntity _theoryHours { get; set; } = new CourseFrameworkChangeableEntityBuilder().WithValue("1").Build();
    public CourseFrameworkChangeableEntity _laboratoryHours { get; set; } = new CourseFrameworkChangeableEntityBuilder().WithValue("1").Build();
    public CourseFrameworkChangeableEntity _personnalWorkHours { get; set; } = new CourseFrameworkChangeableEntityBuilder().WithValue("1").Build();
    public CourseFrameworkChangeableEntity _semester { get; set; } = new CourseFrameworkChangeableEntityBuilder().WithValue("1").Build();
    public string _finalCourseObjective { get; set; } = "";
    public string _courseCharacteristics { get; set; } = "";
    /// <summary>
    /// Permet d'ajouter des sections supplémentaires propres aux différents départements sous la forme d'un texte
    /// </summary>
    public string _otherSpecifications { get; set; } = "";
    public string _statementOfComplexAuthenticTask { get; set; } = "";
    public string _taskPresentation { get; set; } = "";

    public CourseFrameworkEntityBuilder WithCode(string code)
    {
        _code = new CourseFrameworkChangeableEntityBuilder().WithValue(code).Build();
        return this;
    }

    public CourseFrameworkEntityBuilder WithName(string name)
    {
        _name = new CourseFrameworkChangeableEntityBuilder().WithValue(name).Build();
        return this;
    }

    public CourseFrameworkEntityBuilder WithCurrentChangeRecord(ChangeRecordEntity changeRecord)
    {
        _changeRecord = changeRecord;
        return this;
    }

    public CourseFrameworkEntity Build()
    {
        return new CourseFrameworkEntity
        {
            Id = _id,
            Name = _name,
            Code = _code,
            TheoryHours = _theoryHours,
            LaboratoryHours = _laboratoryHours,
            PersonnalWorkHours = _personnalWorkHours,
            Semester = _semester,
            AssedElements = new List<ChangeableEntity>(),
            ChangeRecord = _changeRecord,
            ProgramOfStudy = _programOfStudy,
            FinalCourseObjective = _finalCourseObjective,
            CourseCharacteristics = _courseCharacteristics,
            OtherSpecifications = _otherSpecifications,
            StatementOfComplexAuthenticTask = _statementOfComplexAuthenticTask,
            CourseFrameworkCompetencies = new List<CourseFrameworkCompetencyEntity>(),
            CourseFrameworkPerformanceCriterias = new List<CourseFrameworkPerformanceCriteriaEntity>(),
            Prerequisites = new List<CourseFrameworkEntity>()
        };
    }
}
