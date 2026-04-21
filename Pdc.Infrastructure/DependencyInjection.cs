using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Identity;
using Pdc.Infrastructure.Mappings;
using Pdc.Infrastructure.Repositories;

namespace Pdc.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        if (!string.IsNullOrEmpty(connectionString) && connectionString.Contains("mode=memory"))
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseLazyLoadingProxies()
                    .UseInMemoryDatabase("TestDataBase")
                    .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning)) // used to prevent break on BeginTransaction calls
                    .EnableSensitiveDataLogging();
            });
        }
        else if (!string.IsNullOrEmpty(connectionString))
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseLazyLoadingProxies()
                .EnableDetailedErrors()
                .UseNpgsql(
                    connectionString,
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
                );
        }

        // Register Repositories
        services.AddScoped<IProgramOfStudyRepository, ProgramOfStudyRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICompetencyRepository, CompetencyRepository>();
        services.AddScoped<IComplementaryInformationRepository, ComplementaryInformationRepository>();
        services.AddScoped<IChangeRecordRepository, ChangeRecordRepository>();
        services.AddScoped<IChangeableRepository, ChangeableRepository>();
        services.AddScoped<IChangeDetailsRepository, ChangeDetailsRepository>();

        services.AddAutoMapper((serviceProvider, automapper) =>
        {
        }, typeof(MappingProfile));
        services.AddScoped<IAuthService, IdentityAuthService>();

        return services;
    }
}