using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.Security;

namespace Pdc.Application.Services.UserService;

public class UserService : IUserService
{
    private readonly IAuthService _authService;

    public UserService(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<User> GetCurrentUserInfoAsync() => await _authService.GetCurrentUserAsync();

    public async Task<bool> CanAccessAdminSectionAsync() => await _authService.IsInRoleAsync("Administrator");
}