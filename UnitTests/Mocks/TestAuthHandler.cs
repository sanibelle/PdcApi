using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.Identity;
using Pdc.Infrastructure.Identity;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Pdc.Tests.Mocks;

public static class MockAuthConfiguration
{
    public static IServiceCollection AddMockAuthentication(
        this IServiceCollection services, IConfiguration configuration)
    {
        // Keep the Identity setup the same as in your Azure AD config
        services.AddIdentity<IdentityUserEntity, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        // Add authorization with the same policy
        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

        // Configure authentication with a mock scheme
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = "MockAuth"; // Our custom mock scheme
        })
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddScheme<AuthenticationSchemeOptions, MockAuthHandler>("MockAuth", options => { });

        services.AddHttpContextAccessor();
        services.AddScoped<IAuthService, IdentityAuthService>();

        return services;
    }

    // Custom mock auth handler
    public class MockAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IServiceProvider _serviceProvider;

        public MockAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            IServiceProvider serviceProvider)
            : base(options, logger, encoder)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // This should never be called directly - authentication is handled by the cookie
            return AuthenticateResult.NoResult();
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            // This is called when authentication is required (challenge)
            // In a real system this would redirect to Azure AD
            // In our mock, we'll create a mock user and sign them in

            // Create a scope to resolve required services
            using var scope = _serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUserEntity>>();
            var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<IdentityUserEntity>>();

            // Create mock claims that mimic what Azure AD would provide
            var claims = new List<Claim>
            {
                new Claim("email", "mock-user@example.com"),
                new Claim("given_name", "Mock"),
                new Claim("family_name", "User"),
                new Claim("name", "Mock User"),
                new Claim("role", Roles.Admin),
            };

            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            // Find or create mock user in the database
            var user = await userManager.FindByLoginAsync("AzureAD", "mock-object-id");
            if (user == null)
            {
                user = new IdentityUserEntity
                {
                    UserName = "mock-user@example.com",
                    Email = "mock-user@example.com",
                    EmailConfirmed = true
                };

                // Reuse your GetDefaultRole logic
                string role = await GetDefaultRoleAsync(userManager);
                await userManager.CreateAsync(user);
                await userManager.AddToRoleAsync(user, role);
                await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, "Mock User"));
                await userManager.AddLoginAsync(user, new UserLoginInfo("AzureAD", "mock-object-id", "Mock User"));
            }

            // Sign in the user - this creates the cookie
            await signInManager.SignInAsync(user, isPersistent: true);

            // Redirect to the return URL (or home if not specified)
            var returnUrl = properties?.RedirectUri ?? "/";
            Response.Redirect(returnUrl);
        }

        private static async Task<string> GetDefaultRoleAsync(UserManager<IdentityUserEntity> userManager)
        {
            var users = await userManager.GetUsersInRoleAsync(Roles.Admin);
            var role = users.Count == 0 ? Roles.Admin : Roles.User;
            return role;
        }
    }
}