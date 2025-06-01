using Pdc.Domain.Models.CourseFramework;

namespace Pdc.Domain.Interfaces.Repositories;

public interface IProgramOfStudyRespository
{
    Task<List<ProgramOfStudy>> GetAll();
    Task<ProgramOfStudy> FindByCode(string code);
    Task<bool> ExistsByCode(string code);
    Task<ProgramOfStudy> Add(ProgramOfStudy entity);
    Task<ProgramOfStudy> Update(ProgramOfStudy entity);
    Task Delete(string code);
}
