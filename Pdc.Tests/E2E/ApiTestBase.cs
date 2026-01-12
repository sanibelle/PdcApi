using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Pdc.Infrastructure.Identity;
using TestDataSeeder;

namespace Pdc.E2ETests;

[TestFixture]
public class ApiTestBase
{
    private static readonly Lazy<WebApplicationFactory<Program>> _lazyFactory =
        new(CreateFactory);
    private static readonly Lazy<Task> _lazyDataSeeding =
        new(SeedDataAsync);

    protected HttpClient _Client;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        _Client = _lazyFactory.Value.CreateClient();
        await _lazyDataSeeding.Value;
        SwitchUserRequestingByRole(Roles.Admin);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _Client?.Dispose();
    }

    /// <summary>
    /// Creates the shared WebApplicationFactory. This method will only be called once.
    /// </summary>
    private static WebApplicationFactory<Program> CreateFactory()
    {
        // Set environment to Test
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
        return new WebApplicationFactory<Program>();
    }


    private static async Task SeedDataAsync()
    {
        using var scope = _lazyFactory.Value.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
        await seeder.SeedAsync();
    }

    /// <summary>
    /// Switch the user for the current test class by modifying HTTP headers with the role
    /// </summary>
    /// <param name="role">The role to switch to</param>
    protected void SwitchUserRequestingByRole(string? role)
    {
        switch (role)
        {
            case Roles.Admin:
                SwitchUserRequesting(DataSeeder.Admin);
                break;
            case null:
                SwitchUserRequesting(DataSeeder.SimpleUser);
                break;
            default:
                throw new ArgumentException($"Invalid role: {role}");
        }
    }

    // Switch the user role for the current test class by modifying HTTP headers
    /// </summary>
    /// <param name="role">The role to switch to</param>
    protected void SwitchUserRequesting(IdentityUser<Guid> user)
    {
        if (_Client.DefaultRequestHeaders.Contains("Test-User"))
        {
            _Client.DefaultRequestHeaders.Remove("Test-User");
        }
        _Client.DefaultRequestHeaders.Add("Test-User", user.UserName);
    }
}
