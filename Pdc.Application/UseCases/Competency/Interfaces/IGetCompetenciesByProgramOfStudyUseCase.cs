using Pdc.Application.DTOS;

namespace Pdc.Application.UseCase;
public interface IGetCompetenciesByProgramOfStudyUseCase
{
    Task<IList<CompetencyDTO>> Execute(string programOfStudyCode);
}