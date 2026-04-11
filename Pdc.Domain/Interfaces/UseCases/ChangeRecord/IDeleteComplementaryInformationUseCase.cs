
<<<<<<<< HEAD:Pdc.Domain/Interfaces/UseCases/Versioning/IDeleteComplementaryInformationUseCase.cs
namespace Pdc.Domain.Interfaces.UseCases.Versioning;
========
namespace Pdc.Domain.Interfaces.UseCases.ChangeRecord;
>>>>>>>> 2f52bb1bcb77bc73a5f532a55cf759a5d4c44124:Pdc.Domain/Interfaces/UseCases/ChangeRecord/IDeleteComplementaryInformationUseCase.cs

public interface IDeleteComplementaryInformationUseCase
{
    Task Execute(Guid id, UserModel currentUser);
}