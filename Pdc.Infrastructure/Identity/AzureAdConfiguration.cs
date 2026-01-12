using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Pdc.Domain.Exceptions;
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
        services.AddIdentity<IdentityUserEntity, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthorizationBuilder()
            .SetDefaultPolicy(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build());

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
            int hours = configuration.GetValue<int>("AuthCookie:HoursTimeSpan");
            bool isSliding = configuration.GetValue<bool>("AuthCookie:IsSliding");
            options.Events = new CookieAuthenticationEvents
            {
                OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                },
                OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                }
            };
            options.ExpireTimeSpan = TimeSpan.FromHours(hours); // Set the expiration time for the cookie
            options.SlidingExpiration = isSliding; // extends the cookie expiration by another ExpireTimeSpan on each request
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
        return services;
    }

    private static async Task SynchronizeAzureAdUser(TokenValidatedContext context)
    {
        UserManager<IdentityUserEntity> userManager = context.HttpContext.RequestServices
            .GetRequiredService<UserManager<IdentityUserEntity>>();

        SignInManager<IdentityUserEntity> signInManager = context.HttpContext.RequestServices
        .GetRequiredService<SignInManager<IdentityUserEntity>>();

        if (context.Principal == null)
        {
            throw new AuthException("context.Principal should not be null");
        }
        string? objectId = context.Principal.FindFirstValue(AzureAdClaimTypes.ObjectId);
        if (string.IsNullOrEmpty(objectId))
        {
            throw new AuthException("Failed to find the azureAdClaimTypes object id");
        }

        userManager.GetUsersInRoleAsync(Roles.Admin).GetAwaiter().GetResult(); // Ensure roles are loaded

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
            await HandleIdentityResultAsync(() => userManager.CreateAsync(user), "Failed to create user");
            if (await NoAdminInUserRoles(userManager))
            {
                await HandleIdentityResultAsync(() => userManager.AddToRoleAsync(user, Roles.Admin), "Failed to add user to role");
            }
            await HandleIdentityResultAsync(() => userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, fullName)), "Failed to add claim");
            await HandleIdentityResultAsync(() => userManager.AddLoginAsync(user, new UserLoginInfo("AzureAD", objectId, user.UserName)), "Failed to add login");
        }
        await signInManager.SignInAsync(user, isPersistent: true);
    }

    private static async Task<bool> NoAdminInUserRoles(UserManager<IdentityUserEntity> userManager)
    {
        var users = await userManager.GetUsersInRoleAsync(Roles.Admin);
        return !users.Any();
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