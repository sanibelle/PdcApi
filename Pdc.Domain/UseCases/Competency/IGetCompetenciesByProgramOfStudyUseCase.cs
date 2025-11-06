using Pdc.Application.DTOS;

namespace Pdc.Domain.UseCases.Competency;
public interface IGetCompetenciesByProgramOfStudyUseCase
{
    Task<IList<CompetencyDTO>> Execute(string programOfStudyCode);
}