namespace Pdc.Domain.Interfaces.UseCases.Version;

public interface IDeleteComplementaryInformationUseCase
{
    Task Execute(Guid id, Models.Security.User currentUser);
}