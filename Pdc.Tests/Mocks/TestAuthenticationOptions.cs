using Microsoft.AspNetCore.Authentication;

namespace Pdc.Tests.Mocks;

public class TestAuthenticationOptions : AuthenticationSchemeOptions
{
    public string DefaultUserName { get; set; } = "TestUser";
}
