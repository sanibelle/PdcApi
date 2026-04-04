
namespace Pdc.Domain.Interfaces.UseCases.ChangeRecord;

public interface IDeleteComplementaryInformationUseCase
{
    Task Execute(Guid id, UserModel currentUser);
}