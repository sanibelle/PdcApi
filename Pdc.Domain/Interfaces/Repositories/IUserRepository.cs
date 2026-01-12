using Pdc.Domain.Models.Security;

namespace Pdc.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetUsersWithRoles();
    Task<User> FindUserById(Guid userId);
    Task AddUserRoles(Guid userId, IList<string> roles);
    Task RemoveUserRoles(Guid userId, IList<string> rolesToRemove);
    Task<IList<string>> FindUserRolesByUserId(Guid userId);
}
