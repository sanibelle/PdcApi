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
                options.UseInMemoryDatabase(connectionString));
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
        services.AddScoped<ICompetencyRespository, CompetencyRepository>();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IAuthService, IdentityAuthService>();

        return services;
    }
}