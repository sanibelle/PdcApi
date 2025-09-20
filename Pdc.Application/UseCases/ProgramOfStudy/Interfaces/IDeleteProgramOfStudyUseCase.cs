namespace Pdc.Application.UseCases;

public interface IDeleteProgramOfStudyUseCase
{
    Task Execute(string code);
}