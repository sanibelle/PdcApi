using Pdc.Domain.Models.CourseFramework;

namespace Pdc.Domain.Interfaces.Repositories;

public interface ICourseFrameworkRepository
{
    Task<bool> ExistsEntityByCode(string courseFrameworkCode);
    Task<CourseFramework> FindByCode(string courseFrameworkCode);
    Task<CourseFramework> FindById(Guid id);
    Task<CourseFramework> Add(CourseFramework courseFramework);
    Task<IList<CourseFramework>> GetByProgramOfStudy(string code);
    Task DeleteById(Guid courseFramekworkId);
}
