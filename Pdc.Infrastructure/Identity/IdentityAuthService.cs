using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        ClaimsPrincipal? user = _httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity?.IsAuthenticated == true)
        {
            throw new EntityNotFoundException(nameof(IdentityUserEntity), "current user not found");
        }


        string? userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            throw new EntityNotFoundException(nameof(IdentityUserEntity), "userId not found");
        }
        IdentityUserEntity? identityUser = await _userManager.FindByIdAsync(userId);

        if (identityUser is null)
            throw new EntityNotFoundException(nameof(IdentityUserEntity), "userId not found");

        IList<string> roles = await _userManager.GetRolesAsync(identityUser);

        return new User
        {
            // TODO valider le concept des noms ici
            Id = identityUser.Id,
            Email = identityUser.Email ?? "",
            //TODO aller chercher / ajouter le nom dans la bd.
            DisplayName = identityUser.UserName ?? ""
        };
    }

    public async Task<bool> IsInRoleAsync(string role)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity?.IsAuthenticated == false)
            return false;

        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            throw new Exception("userId is null");
        }
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
        var identityUser = await _userManager.FindByIdAsync(userId);
        if (identityUser == null)
            throw new EntityNotFoundException(nameof(IdentityUserEntity), userId);

        var result = await _userManager.AddToRoleAsync(identityUser, role);
    }
}
