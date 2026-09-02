namespace Pdc.Domain.Interfaces.UseCases.CourseFramework;

using Pdc.Application.DTOS.CourseFramework;
using Pdc.Domain.Models.Security;


public interface IAddCourseFrameworkUseCase
{
    Task<TrackedCourseFrameworkDTO> Execute(string programOfStudyCode, CreateCourseFrameworkDTO courseFramework, User createdBy);
}