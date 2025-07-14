using Pdc.Application.DTOS;

namespace Pdc.Application.UseCase;
public interface IGetCompetenciesUseCase
{
    Task<IList<CompetencyDTO>> Execute(string programOfStudyCode);
}