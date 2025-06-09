using Microsoft.AspNetCore.Authentication;

namespace Pdc.Infrastructure.Identity.TestAuthentication;

public class TestAuthenticationOptions : AuthenticationSchemeOptions
{
    public string DefaultUserName { get; set; } = "TestUser";
}
