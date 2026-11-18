namespace Pdc.Application.DTOS.CourseFramework;

public class UnTrackedCourseFrameworkDTO
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public int TheoryHours { get; set; }
    public int LaboratoryHours { get; set; }
    public int PersonnalWorkHours { get; set; }
    public int Semester { get; set; }
    public Guid Id { get; set; }
    public CompetencyDTO[] Competencies { get; set; } = [];
}