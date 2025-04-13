using Pdc.Domain.Models.Security;

namespace Pdc.Domain.Interfaces.Repositories;

public interface IAuthService
{
    Task<User> GetCurrentUserAsync();
    Task<bool> IsInRoleAsync(string role);
    Task<bool> AuthorizeAsync(string policy);
    Task AssignRoleAsync(string userId, string role);
}
