namespace Pdc.Application.UseCase;
using Pdc.Application.DTOS;

public interface IUpdateCompetencyUseCase
{
    Task<CompetencyDTO> Execute(string programOfStudyCode, CompetencyDTO programOfStudy);
}