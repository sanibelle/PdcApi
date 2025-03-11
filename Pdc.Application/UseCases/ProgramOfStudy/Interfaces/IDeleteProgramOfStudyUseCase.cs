namespace Pdc.Application.UseCase;

public interface IDeleteProgramOfStudyUseCase
{
    Task Execute(Guid id);
}