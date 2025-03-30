using Pdc.Application.DTOS;

namespace Pdc.Application.UseCase;

public interface IGetCompetencyUseCase
{
    Task<CompetencyDTO> Execute(string programOfStudyCode, string competencyCode);
}