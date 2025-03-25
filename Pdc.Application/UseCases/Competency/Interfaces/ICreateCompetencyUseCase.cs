namespace Pdc.Application.UseCase;
using Pdc.Application.DTOS;

public interface ICreateCompetencyUseCase
{
    Task<CompetencyDTO> Execute(string studyProgramCode, CreateCompetencyDTO programOfStudy);
}