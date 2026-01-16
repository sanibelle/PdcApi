using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Models.Security;

public interface ISetUserRolesUseCase
{
    Task<UserDTO> Execute(Guid userId, string[] roles, User currentUser);
}