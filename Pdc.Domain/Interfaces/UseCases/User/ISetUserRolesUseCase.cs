using Pdc.Application.DTOS.Common;

namespace Pdc.Domain.Interfaces.UseCases.User;
public interface ISetUserRolesUseCase
{
    Task<UserDTO> Execute(Guid UserId, string[] roles);
}