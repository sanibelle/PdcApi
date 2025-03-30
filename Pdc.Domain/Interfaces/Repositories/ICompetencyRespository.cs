using Pdc.Domain.Entities.MinisterialSpecification;

namespace Pdc.Domain.Interfaces.Repositories;

public interface ICompetencyRespository
{
    Task<List<MinisterialCompetency>> GetAll();
    Task<MinisterialCompetency> FindByCode(string programOfStudyCode, string competencyCode);
    Task<MinisterialCompetency> Add(MinisterialCompetency entity);
    Task<MinisterialCompetency> Update(MinisterialCompetency entity);
    Task Delete(string programOfStudyCode, string competencyCode);
}
