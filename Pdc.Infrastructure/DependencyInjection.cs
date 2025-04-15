using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Mappings;
using Pdc.Infrastructure.Repositories;

namespace Pdc.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (environment == "Test")
        {
            // Register InMemory DbContext for tests
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("InMemoryTestDatabase")
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging());
        }
        else
        {
            // Register SQL Server DbContext for production
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        }

        // Register Repositories
        services.AddScoped<IProgramOfStudyRespository, ProgramOfStudyRespository>();
        services.AddScoped<ICompetencyRespository, CompetencyRepository>();
        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}