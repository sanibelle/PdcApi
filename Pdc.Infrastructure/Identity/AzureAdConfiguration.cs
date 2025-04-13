using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.Identity;
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

        // Configuration Azure AD
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
        {
            options.Authority = $"{configuration["AzureAd:Instance"]}{configuration["AzureAd:TenantId"]}";
            options.ClientId = configuration["AzureAd:ClientId"];
            options.ClientSecret = configuration["AzureAd:ClientSecret"];
            options.ResponseType = OpenIdConnectResponseType.Code;
            options.CallbackPath = configuration["AzureAd:CallbackPath"];
            options.Scope.Add("openid");
            options.Scope.Add("profile");
            options.Scope.Add("email");
            options.SaveTokens = true;

            options.Events = new OpenIdConnectEvents
            {
                OnTokenValidated = async context =>
                {
                    // Synchroniser l'utilisateur avec Identity mais sans exposer ces détails
                    await SynchronizeAzureAdUser(context);
                }
            };
        });

        services.AddHttpContextAccessor();
        services.AddScoped<IAuthService, IdentityAuthService>();

        return services;
    }

    private static async Task SynchronizeAzureAdUser(TokenValidatedContext context)
    {
        var userManager = context.HttpContext.RequestServices
            .GetRequiredService<UserManager<IdentityUserEntity>>();
        // TODO ameliorer lagestion des erreurs
        if (context.Principal == null)
        {
            throw new Exception("context.Principal should not be null");
        }
        string? objectId = context.Principal.FindFirstValue("http://schemas.microsoft.com/identity/claims/objectidentifier");
        if (objectId == null)
        {
            throw new Exception("Failed to find the object id");
        }

        IdentityUserEntity? user = await userManager.FindByLoginAsync("AzureAD", objectId);
        if (user == null)
        {
            string? email = context.Principal.FindFirstValue(ClaimTypes.Email) ??
                        context.Principal.FindFirstValue("preferred_username");
            string? name = context.Principal.FindFirstValue(ClaimTypes.Name) ??
                        context.Principal.FindFirstValue("name");

            //TODO decoder le user ici.
            // Création de l'utilisateur dans Identity
            user = new IdentityUserEntity
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                NormalizedUserName = name,

            };

            await userManager.CreateAsync(user);
            await userManager.AddLoginAsync(user, new UserLoginInfo("AzureAD", objectId, "AzureAD"));

            // Attribuer des rôles par défaut si nécessaire
            var defaultRole = "User";
            if (!(await userManager.IsInRoleAsync(user, defaultRole)))
            {
                await userManager.AddToRoleAsync(user, defaultRole);
            }
        }
    }
}