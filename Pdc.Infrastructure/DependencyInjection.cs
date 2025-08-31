using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Microsoft.EntityFrameworkCore;
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
                options.UseInMemoryDatabase("TestDataBase")
                .EnableSensitiveDataLogging());
        }
        else if (!string.IsNullOrEmpty(connectionString))
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    connectionString,
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        }

        // Register Repositories
        services.AddScoped<IProgramOfStudyRespository, ProgramOfStudyRespository>();
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