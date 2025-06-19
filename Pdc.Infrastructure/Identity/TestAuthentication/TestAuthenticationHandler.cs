using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pdc.Infrastructure.Entities.Identity;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Pdc.Infrastructure.Identity.TestAuthentication;

public class TestAuthenticationHandler : AuthenticationHandler<TestAuthenticationOptions>
{
    private readonly UserManager<IdentityUserEntity> _userManager;

    public TestAuthenticationHandler(
        IOptionsMonitor<TestAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        UserManager<IdentityUserEntity> userManager)
        : base(options, logger, encoder)
    {
        _userManager = userManager;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Check if there is a username in the request headers for testing
        var userName = Request.Headers["Test-User"].FirstOrDefault() ?? Options.DefaultUserName;

        // Get the user from the database
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return AuthenticateResult.Fail($"Test user {userName} not found");
        }

        // Get user claims from the user manager
        var claims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        // Add standard claims
        var claimsList = new List<Claim>(claims)
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        if (user.UserName is not null)
        {
            claimsList.Add(new(ClaimTypes.Name, user.UserName));

        }

        // Add role claims
        foreach (var role in roles)
        {
            claimsList.Add(new Claim(ClaimTypes.Role, role));
        }

        var identity = new ClaimsIdentity(claimsList, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "Test");

        return AuthenticateResult.Success(ticket);
    }
}

