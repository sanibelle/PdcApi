using Pdc.Application.DTOS.CourseFramework;
using Pdc.Domain.DTOS.Common;

namespace TestDataSeeder.Builders.DTOS;

public class TrackedCourseFrameworkDTOBuilder
{
    private Guid? _id = null;
    private int _changeRecordNumber = 1;
    private bool _isDraft = true;
    private ChangeableDTO<int> _laboratoyHours = new ChangeableDTOBuilder<int>().WithValue(1).Build();
    private ChangeableDTO<int> _theoryHours = new ChangeableDTOBuilder<int>().WithValue(1).Build();
    private ChangeableDTO<int> _semester = new ChangeableDTOBuilder<int>().WithValue(1).Build();
    private ChangeableDTO<int> _personnalWorkHours = new ChangeableDTOBuilder<int>().WithValue(1).Build();
    private ChangeableDTO<string> _code = new ChangeableDTOBuilder<string>().WithValue("Default code").Build();
    private ChangeableDTO<string> _name = new ChangeableDTOBuilder<string>().WithValue("Default name").Build();

    public TrackedCourseFrameworkDTOBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public TrackedCourseFrameworkDTOBuilder WithCode(string code)
    {
        _code.Value = code;
        return this;
    }

    public TrackedCourseFrameworkDTOBuilder WithCode(ChangeableDTO<string> code)
    {
        _code = code;
        return this;
    }

    public TrackedCourseFrameworkDTOBuilder WithLaboratoryHours(int laboratoryHours)
    {
        _laboratoyHours.Value = laboratoryHours;
        return this;
    }

    public TrackedCourseFrameworkDTOBuilder WithLaboratoryHours(ChangeableDTO<int> laboratoryHours)
    {
        _laboratoyHours = laboratoryHours;
        return this;
    }

    public TrackedCourseFrameworkDTOBuilder WithTheoryHours(int theoryHours)
    {
        _theoryHours.Value = theoryHours;
        return this;
    }

    public TrackedCourseFrameworkDTOBuilder WithName(string name)
    {
        _name.Value = name;
        return this;
    }

    public TrackedCourseFrameworkDTOBuilder WithPersonnalWorkHours(int personnalWorkHours)
    {
        _personnalWorkHours.Value = personnalWorkHours;
        return this;
    }

    public TrackedCourseFrameworkDTOBuilder WithSemester(int semester)
    {
        _semester.Value = semester;
        return this;
    }

    public TrackedCourseFrameworkDTOBuilder WithTheoryHours(ChangeableDTO<int> theoryHours)
    {
        _theoryHours = theoryHours;
        return this;
    }

    public TrackedCourseFrameworkDTOBuilder WithName(ChangeableDTO<string> name)
    {
        _name = name;
        return this;
    }

    public TrackedCourseFrameworkDTOBuilder WithPersonnalWorkHours(ChangeableDTO<int> personnalWorkHours)
    {
        _personnalWorkHours = personnalWorkHours;
        return this;
    }

    public TrackedCourseFrameworkDTOBuilder WithSemester(ChangeableDTO<int> semester)
    {
        _semester = semester;
        return this;
    }

    public TrackedCourseFrameworkDTOBuilder WithChangeRecordNumber(int changeRecordNumber)
    {
        _changeRecordNumber = changeRecordNumber;
        return this;
    }

    public TrackedCourseFrameworkDTOBuilder WithIsDraft(bool isDraft)
    {
        _isDraft = isDraft;
        return this;
    }

    public TrackedCourseFrameworkDTO Build()
    {
        return new TrackedCourseFrameworkDTO
        {
            Id = _id.HasValue ? _id.Value : throw new Exception("You need a GUID bro"),
            Code = _code,
            Name = _name,
            Weighting = new WeightingDTO(_theoryHours, _laboratoyHours, _personnalWorkHours),
            Semester = _semester,
            IsDraft = _isDraft,
            ChangeRecordNumber = _changeRecordNumber,
        };
    }

}
