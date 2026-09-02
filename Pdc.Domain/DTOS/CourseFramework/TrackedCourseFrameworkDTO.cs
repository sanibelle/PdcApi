using Pdc.Domain.DTOS.Common;

namespace Pdc.Application.DTOS.CourseFramework;

public class TrackedCourseFrameworkDTO
{
    public Guid Id { get; set; }
    public required ChangeableDTO<string> Code { get; set; }
    public required ChangeableDTO<string> Name { get; set; }
    public required ChangeableDTO<int> Semester { get; set; }
    public DateTime CreatedOn { get; set; }
    public required WeightingDTO Weighting { get; set; }
    public int ChangeRecordNumber { get; set; }
    public bool IsDraft { get; set; }
}