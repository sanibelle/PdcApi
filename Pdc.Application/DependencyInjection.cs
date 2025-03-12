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
        services.AddScoped<ICreateProgramOfStudyUseCase, CreateProgramOfSudy>();
        services.AddScoped<IDeleteProgramOfStudyUseCase, DeleteProgramOfSudy>();
        services.AddScoped<IGetAllProgramOfStudyUseCase, GetAllProgramOfSudy>();
        services.AddScoped<IUpdateProgramOfStudyUseCase, UpdateProgramOfSudy>();
        services.AddScoped<IGetProgramOfStudyUseCase, GetProgramOfSudy>();

        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}