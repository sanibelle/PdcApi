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


        string? userId = user.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new ClaimNotFoundException(ClaimTypes.NameIdentifier);

        IdentityUserEntity? identityUser = await _userManager.FindByIdAsync(userId)
            ?? throw new EntityNotFoundException(nameof(IdentityUserEntity), "userId not found");

        IList<Claim> claims = await _userManager.GetClaimsAsync(identityUser)
            ?? throw new ClaimNotFoundException("ClaimsList");

        var claim = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)
            ?? throw new ClaimNotFoundException(ClaimTypes.Name);

        return new User
        {
            Id = identityUser.Id,
            Email = identityUser.Email,
            DisplayName =  claim.Value
        };
    }

    public async Task<bool> IsInRoleAsync(string role)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity?.IsAuthenticated == false)
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
        var identityUser = await _userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException(nameof(IdentityUserEntity), userId);
        var result = await _userManager.AddToRoleAsync(identityUser, role);
    }
}
