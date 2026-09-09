using Pdc.Domain.DTOS.Common;

namespace Pdc.Application.DTOS.CourseFramework;

public class WeightingDTO
{
    public ChangeableDTO<int> TheoryHours { get; set; }
    public ChangeableDTO<int> LaboratoryHours { get; set; }
    public ChangeableDTO<int> PersonnalWorkHours { get; set; }

    public WeightingDTO(ChangeableDTO<int> theoryHours, ChangeableDTO<int> laboratoryHours, ChangeableDTO<int> personnalWorkHours)
    {
        TheoryHours = theoryHours;
        LaboratoryHours = laboratoryHours;
        PersonnalWorkHours = personnalWorkHours;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public WeightingDTO()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }
}