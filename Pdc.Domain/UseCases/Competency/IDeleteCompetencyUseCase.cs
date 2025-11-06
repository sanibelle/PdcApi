namespace Pdc.Domain.UseCases.Competency;

public interface IDeleteCompetencyUseCase
{
    Task Execute(string programOfStudyCode, string competencyCode);
}