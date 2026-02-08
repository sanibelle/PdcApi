using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
       IConfiguration configuration,
       IHostEnvironment environment)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        if (!string.IsNullOrEmpty(connectionString) && connectionString.Contains("mode=memory"))
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDataBase")
                    .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning)) // used to prevent break on BeginTransaction calls
                    .EnableSensitiveDataLogging();
            });
        }
        else if (!string.IsNullOrEmpty(connectionString))
        {
            services.AddDbContext<AppDbContext>(options =>

                options.UseNpgsql(
                    connectionString,
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
                    .ConfigureWarnings(warnings =>
                        warnings.Ignore(RelationalEventId.PendingModelChangesWarning))
                );
        }

        // Register Repositories
        services.AddScoped<IProgramOfStudyRepository, ProgramOfStudyRespository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICompetencyRepository, CompetencyRepository>();
        services.AddAutoMapper((serviceProvider, automapper) =>
        {
            automapper.AddCollectionMappers();
            automapper.UseEntityFrameworkCoreModel<AppDbContext>(serviceProvider);
        }, typeof(MappingProfile));
        services.AddScoped<IAuthService, IdentityAuthService>();

        return services;
    }
}