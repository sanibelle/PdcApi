using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.Identity;
using Pdc.Infrastructure.Identity;
using Pdc.Tests.Mocks;

namespace Pdc.E2ETests;

[TestFixture]
public class ApiTestBase
{
    protected WebApplicationFactory<Program> _factory;
    protected HttpClient _client;
    protected AppDbContext _dbContext;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        // Set environment to Test
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");

        _factory = new WebApplicationFactory<Program>()
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
                    // Register TestDataSeeder
                    services.AddScoped<TestDataSeeder>();

                    // Replace the existing authentication with test authentication
                    services.AddTestAuthentication();
                });
            });

        // Create an HttpClient that uses the factory
        _client = _factory.CreateClient();

        // Retrieve the AppDbContext from the factory's service provider
        using (var scope = _factory.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUserEntity>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            _dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Seed the test database
            var seeder = scope.ServiceProvider.GetRequiredService<TestDataSeeder>();
            await seeder.SeedTestData(userManager, roleManager);
            SwitchUserRole(Roles.Admin);
        }
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        // With in-memory databases, we don't need to call EnsureDeleted()
        _client.Dispose();
        _factory.Dispose();
    }

    /// <summary>
    /// Login through the authentication controller
    /// </summary>
    /// <param name="user">The user to login</param>
    /// <returns>The HTTP response from the login endpoint</returns>
    protected void SwitchUserRole(string role)
    {

        if (_client.DefaultRequestHeaders.Contains("Test-User"))
        {
            _client.DefaultRequestHeaders.Remove("Test-User");
        }

        switch (role)
        {
            case Roles.Admin:
                _client.DefaultRequestHeaders.Add("Test-User", TestDataSeeder.Admin.UserName);
                break;
            case Roles.User:
                _client.DefaultRequestHeaders.Add("Test-User", TestDataSeeder.User.UserName);
                break;
            default:
                throw new ArgumentException($"Invalid role: {role}");
        }
    }
}
