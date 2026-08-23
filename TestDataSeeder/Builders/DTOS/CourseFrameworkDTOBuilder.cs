using Pdc.Application.DTOS.CourseFramework;

namespace TestDataSeeder.Builders.DTOS;

public class CourseFrameworkDTOBuilder
{
    private Guid? _id = null;
    private int _laboratoyHours = 1;
    private int _theoryHours = 1;
    private int _semester = 1;
    private int _personnalWorkHours = 1;
    private string _code = "Default code";
    private string _name = "Default name";

    public CourseFrameworkDTOBuilder WithId(Guid? id)
    {
        _id = id;
        return this;
    }

    public CourseFrameworkDTOBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    public CourseFrameworkDTOBuilder WithLaboratoryHours(int laboratoryHours)
    {
        _laboratoyHours = laboratoryHours;
        return this;
    }

    public CourseFrameworkDTOBuilder WithTheoryHours(int theoryHours)
    {
        _theoryHours = theoryHours;
        return this;
    }

    public CourseFrameworkDTOBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public CourseFrameworkDTOBuilder WithPersonnalWorkHours(int personnalWorkHours)
    {
        _personnalWorkHours = personnalWorkHours;
        return this;
    }

    public CourseFrameworkDTOBuilder WithSemester(int semester)
    {
        _semester = semester;
        return this;
    }

    public CreateCourseFrameworkDTO Build()
    {
        return new CreateCourseFrameworkDTO
        {
            Code = _code,
            Name = _name,
            LaboratoryHours = _laboratoyHours,
            TheoryHours = _theoryHours,
            PersonnalWorkHours = _personnalWorkHours,
            Semester = _semester

        };
    }

}
