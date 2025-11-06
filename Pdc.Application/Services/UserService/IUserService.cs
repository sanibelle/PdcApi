
using Pdc.Domain.Models.Security;

namespace Pdc.Application.Services.UserService;

public interface IUserService
{
    Task<User> GetCurrentUserInfoAsync();

    Task<bool> CanAccessAdminSectionAsync();
}