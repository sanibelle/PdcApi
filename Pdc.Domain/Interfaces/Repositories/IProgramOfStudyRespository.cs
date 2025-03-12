using Pdc.Domain.Entities.CourseFramework;

namespace Pdc.Domain.Interfaces.Repositories;

public interface IProgramOfStudyRespository
{
    Task<List<ProgramOfStudy>> GetAll();
    Task<ProgramOfStudy> FindById(Guid id);
    Task<ProgramOfStudy> Add(ProgramOfStudy entity);
    Task<ProgramOfStudy> Update(ProgramOfStudy entity);
    Task Delete(Guid id);
}
