using Pdc.Domain.DTOS.Common;

namespace Pdc.Domain.Interfaces.UseCases.User;

public interface ISetUserRolesUseCase
{
    Task<UserDTO> Execute(Guid userId, string[] roles, Models.Security.User currentUser);
}