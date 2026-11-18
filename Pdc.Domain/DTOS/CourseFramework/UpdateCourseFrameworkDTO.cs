using Pdc.Domain.DTOS.Common;

namespace Pdc.Application.DTOS.CourseFramework;

public class UpdateCourseFrameworkDTO
{
    public required ChangeableDTO<string> Code { get; set; }
    public required ChangeableDTO<string> Name { get; set; }
    public required ChangeableDTO<int> Semester { get; set; }
    public required WeightingDTO Weighting { get; set; }
    public int ChangeRecordNumber { get; set; }
    public bool IsDraft { get; set; }
    public string[] CompetencyCodes { get; set; } = [];
}