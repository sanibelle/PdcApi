
using Pdc.Domain.Models.Security;

namespace Pdc.Application.Services.UserService;

// TODO changer ca de place I guess
public interface IUserService
{
    Task<User> GetCurrentUserInfoAsync();

    Task<bool> CanAccessAdminSectionAsync();
}