using Pdc.Application.DTOS.CourseFramework;
using Pdc.Domain.DTOS.Common;

namespace TestDataSeeder.Builders.DTOS;

public class UpdateCourseFrameworkDTOBuilder
{
    private WeightingDTO _weighting;
    private ChangeableDTO<int> _semester;
    private ChangeableDTO<string> _code;
    private ChangeableDTO<string> _name;
    private string[] _competencyCodes = [];
    private bool _isDraft = false;
    private int _changeRecordNumber = 1;

    public UpdateCourseFrameworkDTOBuilder(TrackedCourseFrameworkDTO? createdCourseFramework)
    {
        _weighting = createdCourseFramework.Weighting;
        _semester = createdCourseFramework.Semester;
        _code = createdCourseFramework.Code;
        _name = createdCourseFramework.Name;
        _competencyCodes = createdCourseFramework.Competencies.Select(c => c.Code).ToArray();
        _isDraft = createdCourseFramework.IsDraft;
        _changeRecordNumber = createdCourseFramework.ChangeRecordNumber;
    }

    public UpdateCourseFrameworkDTOBuilder UpdateCode(string code)
    {
        _code.Value = code;
        return this;
    }

    public UpdateCourseFrameworkDTOBuilder UpdateLaboratoryHours(int laboratoryHours)
    {
        _weighting.LaboratoryHours.Value = laboratoryHours;
        return this;
    }

    public UpdateCourseFrameworkDTOBuilder UpdateTheoryHours(int theoryHours)
    {
        _weighting.TheoryHours.Value = theoryHours;
        return this;
    }

    public UpdateCourseFrameworkDTOBuilder UpdateName(string name)
    {
        _name.Value = name;
        return this;
    }

    public UpdateCourseFrameworkDTOBuilder UpdatePersonnalWorkHours(int personnalWorkHours)
    {
        _weighting.PersonnalWorkHours.Value = personnalWorkHours;
        return this;
    }

    public UpdateCourseFrameworkDTOBuilder UpdateSemester(int semester)
    {
        _semester.Value = semester;
        return this;
    }

    public UpdateCourseFrameworkDTOBuilder WithCompetencyCodes(string[] competencyCodes)
    {
        _competencyCodes = competencyCodes;
        return this;
    }

    public UpdateCourseFrameworkDTO Build()
    {
        return new UpdateCourseFrameworkDTO
        {
            Code = _code,
            Name = _name,
            Semester = _semester,
            Weighting = _weighting,
            IsDraft = _isDraft,
            ChangeRecordNumber = _changeRecordNumber,
            CompetencyCodes = _competencyCodes
        };
    }

}
