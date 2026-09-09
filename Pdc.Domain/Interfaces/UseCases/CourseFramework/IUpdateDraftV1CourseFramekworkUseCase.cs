namespace Pdc.Domain.Interfaces.UseCases.CourseFramework;

using Pdc.Application.DTOS.CourseFramework;
using Pdc.Domain.Models.Security;

public interface IUpdateDraftV1CourseFramekworkUseCase
{
    Task<TrackedCourseFrameworkDTO> Execute(Guid courseFrameworkId, TrackedCourseFrameworkDTO updateCourseFrameworkDto, User currentUser);
}