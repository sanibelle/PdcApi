using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Pdc.Infrastructure.Data;
using System;
using System.Net.Http;

namespace Pdc.E2ETests;

[TestFixture]
public class ApiTestBase
{
    protected WebApplicationFactory<Program> _factory;
    protected HttpClient _client;
    protected AppDbContext _dbContext;

    [OneTimeSetUp]
    public void OneTimeSetUp()
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
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                          .AddEnvironmentVariables();
                });

                builder.ConfigureServices(services =>
                {
                    // Register TestDataSeeder
                    services.AddScoped<TestDataSeeder>();
                });
            });

        // Create an HttpClient that uses the factory
        _client = _factory.CreateClient();

        // Retrieve the AppDbContext from the factory's service provider
        using (var scope = _factory.Services.CreateScope())
        {
            _dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Seed the test database
            var seeder = scope.ServiceProvider.GetRequiredService<TestDataSeeder>();
            seeder.SeedTestData().Wait();
        }
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        // With in-memory databases, we don't need to call EnsureDeleted()
        _client.Dispose();
        _factory.Dispose();
    }
}
