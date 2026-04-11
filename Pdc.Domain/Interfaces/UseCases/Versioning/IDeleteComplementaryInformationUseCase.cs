
namespace Pdc.Domain.Interfaces.UseCases.Versioning;

public interface IDeleteComplementaryInformationUseCase
{
    Task Execute(Guid id, UserModel currentUser);
}