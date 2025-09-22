using Pdc.Application.DTOS;

namespace Pdc.Application.UseCases;

public interface IGetCompetencyUseCase
{
    Task<CompetencyDTO> Execute(string programOfStudyCode, string competencyCode);
}