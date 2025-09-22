using Pdc.Application.DTOS;

namespace Pdc.Application.UseCases;
public interface IGetCompetenciesByProgramOfStudyUseCase
{
    Task<IList<CompetencyDTO>> Execute(string programOfStudyCode);
}