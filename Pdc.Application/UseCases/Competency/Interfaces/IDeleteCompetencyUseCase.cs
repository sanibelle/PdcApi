namespace Pdc.Application.UseCase;

public interface IDeleteCompetencyUseCase
{
    Task Execute(string code);
}