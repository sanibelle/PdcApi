using AutoMapper;
using AutoMapper.EquivalencyExpression;
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
                .UseNpgsql(
                    connectionString,
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
                );
        }

        // Register Repositories
        services.AddScoped<IProgramOfStudyRepository, ProgramOfStudyRespository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICompetencyRepository, CompetencyRepository>();
        services.AddScoped<IComplementaryInformationRepository, ComplementaryInformationRepository>();
        services.AddScoped<IVersionRepository, VersionRepository>();

        services.AddAutoMapper((serviceProvider, automapper) =>
        {
            automapper.AddCollectionMappers();
            automapper.UseEntityFrameworkCoreModel<AppDbContext>(serviceProvider);
        }, typeof(MappingProfile));
        services.AddScoped<IAuthService, IdentityAuthService>();

        return services;
    }
}