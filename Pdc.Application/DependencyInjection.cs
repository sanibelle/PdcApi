using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Pdc.Application.Mappings;
using Pdc.Application.Services.UserService;
using Pdc.Application.UseCases;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Competency;
using Pdc.Domain.Interfaces.UseCases.ProgramOfStudy;
using Pdc.Domain.Interfaces.UseCases.Role;
using Pdc.Domain.Interfaces.UseCases.User;
using Pdc.Infrastructure.Repositories;
using System.Reflection;

namespace Pdc.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        // ProgramOfStudy
        services.AddScoped<ICreateProgramOfStudyUseCase, CreateProgramOfStudy>();
        services.AddScoped<IDeleteProgramOfStudyUseCase, DeleteProgramOfStudy>();
        services.AddScoped<IGetProgramOfStudiesUseCase, GetProgramOfStudies>();
        services.AddScoped<IUpdateProgramOfStudyUseCase, UpdateProgramOfStudy>();
        services.AddScoped<IGetProgramOfStudyUseCase, GetProgramOfStudy>();
        //Competency
        services.AddScoped<ICreateCompetencyUseCase, CreateCompetency>();
        services.AddScoped<IGetCompetencyUseCase, GetCompetency>();
        services.AddScoped<IGetCompetenciesByProgramOfStudyUseCase, GetCompetenciesByProgramOfStudy>();
        services.AddScoped<IUpdateDraftV1CompetencyUseCase, UpdateDraftV1Competency>();
        services.AddScoped<IDeleteCompetencyUseCase, DeleteCompetency>();
        //User
        services.AddScoped<IGetUsersUseCase, GetUsers>();
        services.AddScoped<ISetUserRolesUseCase, SetUserRoles>();
        services.AddScoped<IGetUserUseCase, GetUser>();
        //Role
        services.AddScoped<IGetRolesUseCase, GetRoles>();

        // Auth
        services.AddScoped<IUserService, UserService>();


        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}