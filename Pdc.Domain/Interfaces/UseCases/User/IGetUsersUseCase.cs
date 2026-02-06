using Pdc.Domain.DTOS.Common;

namespace Pdc.Domain.Interfaces.UseCases.User;
public interface IGetUsersUseCase
{
    Task<IList<UserDTO>> Execute();
}