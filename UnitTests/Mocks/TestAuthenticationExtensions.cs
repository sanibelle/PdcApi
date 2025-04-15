using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.Identity;

namespace Pdc.Tests.Mocks;

public static class TestAuthenticationExtensions
{
    public static IServiceCollection AddTestAuthentication(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUserEntity, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Test";
            options.DefaultChallengeScheme = "Test";
            options.DefaultScheme = "Test";
        })
        .AddScheme<TestAuthenticationOptions, TestAuthenticationHandler>("Test", options => { });

        return services;
    }
}
