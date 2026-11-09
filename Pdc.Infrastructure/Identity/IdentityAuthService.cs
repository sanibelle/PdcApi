using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.Security;
using Pdc.Infrastructure.Entities.Identity;
using Pdc.Infrastructure.Exceptions;
using System.Security.Claims;

namespace Pdc.Infrastructure.Identity;

public class IdentityAuthService : IAuthService
{
    private UserManager<IdentityUserEntity> _userManager;
    private SignInManager<IdentityUserEntity> _signInManager;
    private IHttpContextAccessor _httpContextAccessor;
    private IAuthorizationService _authorizationService;

    public IdentityAuthService(
        UserManager<IdentityUserEntity> userManager,
        SignInManager<IdentityUserEntity> signInManager,
        IHttpContextAccessor httpContextAccessor,
        IAuthorizationService authorizationService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
        _authorizationService = authorizationService;
    }

    public async Task<User> GetCurrentUserAsync()
    {
        ClaimsPrincipal? userFromHttpContext = _httpContextAccessor.HttpContext?.User;
        if (userFromHttpContext == null || userFromHttpContext.Identity?.IsAuthenticated != true)
        {
            throw new NotFoundException(nameof(IdentityUserEntity), "current user not found");
        }

        string? userId = userFromHttpContext.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new ClaimNotFoundException(ClaimTypes.NameIdentifier);

        IdentityUserEntity? user = await _userManager.FindByIdAsync(userId)
            ?? throw new NotFoundException(nameof(IdentityUserEntity), "userId not found");

        IList<Claim> claims = await _userManager.GetClaimsAsync(user)
            ?? throw new ClaimNotFoundException("ClaimsList");

        string userName = user.UserName
            ?? claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value
            ?? throw new ClaimNotFoundException(ClaimTypes.Name);

        IList<string> roles = await _userManager.GetRolesAsync(user);

        return new User
        {
            Id = user.Id,
            Email = user.Email,
            UserName =  user.UserName!,
            Roles = roles,
        };
    }

    public async Task<bool> IsInRoleAsync(string role)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || user.Identity?.IsAuthenticated != true)
            return false;

        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("userId is null");
        var identityUser = await _userManager.FindByIdAsync(userId);

        return identityUser != null && await _userManager.IsInRoleAsync(identityUser, role);
    }

    public async Task<bool> AuthorizeAsync(string policy)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
            return false;

        var result = await _authorizationService.AuthorizeAsync(user, policy);
        return result.Succeeded;
    }

    public async Task AssignRoleAsync(string userId, string role)
    {
        var identityUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(nameof(IdentityUserEntity), userId);
        var result = await _userManager.AddToRoleAsync(identityUser, role);
        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
    }
}
