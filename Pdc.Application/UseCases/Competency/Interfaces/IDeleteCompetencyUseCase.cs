namespace Pdc.Application.UseCases;

public interface IDeleteCompetencyUseCase
{
    Task Execute(string programOfStudyCode, string competencyCode);
}