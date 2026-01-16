using Pdc.Domain.Models.Security;

namespace Pdc.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetUsersWithRoles();
    Task<User> FindUserById(Guid userId);
    Task<User> SetUserRoles(Guid userId, IList<string> rolesToAdd, IList<string> rolesToRemove);
    Task<IList<string>> FindUserRolesByUserId(Guid userId);
    Task<IList<string>> GetAllRoles();
    bool IsAdminRoleInArray(IList<string> roles);
}
