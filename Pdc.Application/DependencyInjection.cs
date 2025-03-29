using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Pdc.Application.Mappings;
using Pdc.Application.UseCase;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Infrastructure.Repositories;
using System.Reflection;

namespace Pdc.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<IProgramOfStudyRespository, ProgramOfStudyRespository>();
        services.AddScoped<ICreateProgramOfStudyUseCase, CreateProgramOfStudy>();
        services.AddScoped<IDeleteProgramOfStudyUseCase, DeleteProgramOfStudy>();
        services.AddScoped<IGetAllProgramOfStudyUseCase, GetAllProgramOfStudy>();
        services.AddScoped<IUpdateProgramOfStudyUseCase, UpdateProgramOfStudy>();
        services.AddScoped<IGetProgramOfStudyUseCase, GetProgramOfStudy>();
        services.AddScoped<ICreateCompetencyUseCase, CreateCompetency>();


        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}