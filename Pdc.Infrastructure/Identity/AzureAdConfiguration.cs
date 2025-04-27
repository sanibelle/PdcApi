using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.Identity;
using Pdc.Infrastructure.Exceptions;
using System.Security.Claims;

namespace Pdc.Infrastructure.Identity;

public static class AzureAdConfiguration
{
    public static IServiceCollection AddAzureAdAuthentication(
        this IServiceCollection services, IConfiguration configuration)
    {
        // TODO mapper IdentityUserEntity
        services.AddIdentity<IdentityUserEntity, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
            options.Events = new CookieAuthenticationEvents
            {
                OnRedirectToLogin = context =>
                {
                    //TODO utile?
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                },
                OnRedirectToAccessDenied = context =>
                {
                    //TODO utile?
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                }
            };
        })
        .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
        {
            options.Authority = $"{configuration["AzureAd:Instance"]}{configuration["AzureAd:TenantId"]}";
            options.ClientId = configuration["AzureAd:ClientId"];
            options.ClientSecret = configuration["AzureAd:ClientSecret"];
            options.ResponseType = OpenIdConnectResponseType.Code;
            options.CallbackPath = configuration["AzureAd:CallbackPath"];
            options.Scope.Add(OpenIdConnectScope.Email);
            options.SaveTokens = false;
            options.GetClaimsFromUserInfoEndpoint = true;
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.Events = new OpenIdConnectEvents
            {
                OnAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                },
                OnRedirectToIdentityProvider = context =>
                {
                    if (context.Request.Path != "/api/auth/login")
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.HandleResponse(); // This is critical - it prevents the redirect
                    }
                    return Task.CompletedTask;
                },
                OnTokenValidated = async context =>
                {
                    // Synchroniser l'utilisateur avec Identity mais sans exposer ces détails
                    await SynchronizeAzureAdUser(context);
                },
                OnAuthorizationCodeReceived = context =>
                {
                    System.Diagnostics.Debug.WriteLine("Authorization code received");
                    return Task.CompletedTask;
                },
                OnRemoteFailure = context =>
                {
                    System.Diagnostics.Debug.WriteLine($"Remote failure: {context.Failure?.Message}");
                    return Task.CompletedTask;
                }
            };
        });

        services.AddHttpContextAccessor();
        services.AddScoped<IAuthService, IdentityAuthService>();
        return services;
    }

    private static async Task SynchronizeAzureAdUser(TokenValidatedContext context)
    {
        UserManager<IdentityUserEntity> userManager = context.HttpContext.RequestServices
            .GetRequiredService<UserManager<IdentityUserEntity>>();

        SignInManager<IdentityUserEntity> signInManager = context.HttpContext.RequestServices
        .GetRequiredService<SignInManager<IdentityUserEntity>>();

        // TODO ameliorer lagestion des erreurs
        if (context.Principal == null)
        {
            throw new Exception("context.Principal should not be null");
        }
        //"a7a5abae-0223-4dd3-b505-a0520aafe834"
        string? objectId = context.Principal.FindFirstValue(AzureAdClaimTypes.ObjectId);
        if (string.IsNullOrEmpty(objectId))
        {
            throw new Exception("Failed to find the object id");
        }

        IdentityUserEntity? user = await userManager.FindByLoginAsync("AzureAD", objectId);
        if (user == null)
        {
            string email = GetEmailFromClaims(context.Principal);
            string fullName = GetUserNameFromClaims(context.Principal);

            user = new IdentityUserEntity
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };
            string role = await GetDefaultRole(userManager);
            await HandleIdentityResultAsync(() => userManager.CreateAsync(user), "Failed to create user");
            await HandleIdentityResultAsync(() => userManager.AddToRoleAsync(user, role), "Failed to add user to role");
            await HandleIdentityResultAsync(() => userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, fullName)), "Failed to add claim");
            await HandleIdentityResultAsync(() => userManager.AddLoginAsync(user, new UserLoginInfo("AzureAD", objectId, user.UserName)), "Failed to add login");
        }
        await signInManager.SignInAsync(user, isPersistent: true);
    }

    private static async Task<string> GetDefaultRole(UserManager<IdentityUserEntity> userManager)
    {
        // TODO ajouter une logique d'init de l'app qui ne fonctionne que la première fois et qui est retirée par la suite pour éviter une attaque par buffre overflow.
        var users = await userManager.GetUsersInRoleAsync(Roles.Admin);
        var role = users.Count == 0 ? Roles.Admin : Roles.User;
        return role;
    }

    private static string GetEmailFromClaims(ClaimsPrincipal principal)
    {
        string[] keys = ["email", ClaimTypes.Email, "preferred_username", ClaimTypes.Upn, "upn"];
        return GetValueFromClaims(principal, keys, "@");
    }

    private static string GetUserNameFromClaims(ClaimsPrincipal principal)
    {
        try
        {
            string givenName = GetValueFromClaims(principal, ["given_name", ClaimTypes.GivenName]);
            string familyName = GetValueFromClaims(principal, ["family_name", ClaimTypes.Surname, "surname"]);
            return $"{givenName} {familyName}";
        }
        catch (ClaimNotFoundException)
        {
            string[] keys = ["name", ClaimTypes.Name, "preferred_username", ClaimTypes.Upn, "upn"];
            return GetValueFromClaims(principal, keys);
        }
    }

    private static string GetValueFromClaims(ClaimsPrincipal principal, string[] keys, string? shouldContains = null)
    {
        string? value = null;
        foreach (string key in keys)
        {
            value = principal.FindFirstValue(key) ?? principal.Claims.FirstOrDefault(x => x.Type ==key)?.Value;
            if (!string.IsNullOrEmpty(value) && (string.IsNullOrEmpty(shouldContains) || value.Contains(shouldContains)))
                return value;
        }
        throw new ClaimNotFoundException($" or does not meet the expected criteria.");
    }

    private static async Task HandleIdentityResultAsync(Func<Task<IdentityResult>> action, string errorMessage)
    {
        var result = await action();
        if (result.Errors.Any())
        {
            throw new Exception($"{errorMessage}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }
}