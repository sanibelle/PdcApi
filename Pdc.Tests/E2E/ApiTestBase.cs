using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pdc.Infrastructure.Identity;
using Pdc.Infrastructure.Identity.TestAuthentication;
using TestDataSeeder;

namespace Pdc.E2ETests;

[TestFixture]
public class ApiTestBase
{
    private static readonly Lazy<WebApplicationFactory<Program>> _lazyFactory =
        new Lazy<WebApplicationFactory<Program>>(CreateFactory);
    private static readonly Lazy<Task> _lazyDataSeeding =
        new Lazy<Task>(SeedDataAsync);

    // Static property to access the shared factory
    protected static WebApplicationFactory<Program> Factory => _lazyFactory.Value;
    protected HttpClient _client;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        // Ensure data seeding is completed (this will only run once across all test classes)
        await _lazyDataSeeding.Value;
        _client = Factory.CreateClient();
        SwitchUserRole(Roles.Admin);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _client?.Dispose();
    }

    /// <summary>
    /// Creates the shared WebApplicationFactory. This method will only be called once.
    /// </summary>
    private static WebApplicationFactory<Program> CreateFactory()
    {
        // Set environment to Test
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");

        return new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    var env = context.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
                          .AddEnvironmentVariables();
                });

                builder.ConfigureServices(services =>
                {
                    services.AddScoped<DataSeeder>();
                    services.AddTestAuthentication();
                });
            });
    }

    private static async Task SeedDataAsync()
    {
        using var scope = Factory.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
        await seeder.SeedAsync();
    }

    /// <summary>
    /// Switch the user role for the current test class by modifying HTTP headers
    /// </summary>
    /// <param name="role">The role to switch to</param>
    protected void SwitchUserRole(string role)
    {
        if (_client.DefaultRequestHeaders.Contains("Test-User"))
        {
            _client.DefaultRequestHeaders.Remove("Test-User");
        }

        switch (role)
        {
            case Roles.Admin:
                _client.DefaultRequestHeaders.Add("Test-User", DataSeeder.Admin.UserName);
                break;
            case Roles.User:
                _client.DefaultRequestHeaders.Add("Test-User", DataSeeder.User.UserName);
                break;
            default:
                throw new ArgumentException($"Invalid role: {role}");
        }
    }
}
