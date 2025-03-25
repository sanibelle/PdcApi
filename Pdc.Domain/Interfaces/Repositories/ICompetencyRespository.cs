using Pdc.Domain.Entities.MinisterialSpecification;

namespace Pdc.Domain.Interfaces.Repositories;

public interface ICompetencyRespository
{
    Task<List<Competency>> GetAll();
    Task<Competency> FindByCode(string code);
    Task<Competency> Add(Competency entity);
    Task<Competency> Update(Competency entity);
    Task Delete(string code);
}
