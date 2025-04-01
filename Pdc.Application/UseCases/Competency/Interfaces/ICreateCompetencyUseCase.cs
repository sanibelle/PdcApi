namespace Pdc.Application.UseCase;
using Pdc.Application.DTOS;

public interface ICreateCompetencyUseCase
{
    Task<CompetencyDTO> Execute(string programOfStudyCode, CompetencyDTO programOfStudy);
}