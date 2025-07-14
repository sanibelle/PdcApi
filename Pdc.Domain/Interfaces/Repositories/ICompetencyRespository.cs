using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Security;

namespace Pdc.Domain.Interfaces.Repositories;

public interface ICompetencyRespository
{
    Task<List<MinisterialCompetency>> GetAll();
    Task<MinisterialCompetency> FindByCode(string programOfStudyCode, string competencyCode);
    Task<MinisterialCompetency> Update(MinisterialCompetency entity);
    Task Delete(string programOfStudyCode, string competencyCode);
    Task<bool> ExistsEntityByCode(string programOfStudyCode, string competencyCode);
    Task<MinisterialCompetency> Add(ProgramOfStudy program, MinisterialCompetency competency, User currentUser);
    Task<List<MinisterialCompetency>> GetByProgramOfStudy(string programOfStudyCode);
}
