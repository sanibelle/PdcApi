using Pdc.Application.DTOS.Common;
using Pdc.Domain.Models.Security;

public interface ISetUserRolesUseCase
{
    Task<UserDTO> Execute(Guid UserId, string[] roles, User currentUser);
}