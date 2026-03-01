using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Pdc.Application.Mappings;
using Pdc.Application.Services.UserService;
using Pdc.Application.UseCases;
using Pdc.Application.UseCases.Competency;
using Pdc.Application.UseCases.Version;
using Pdc.Domain.Interfaces.UseCases.Competency;
using Pdc.Domain.Interfaces.UseCases.ProgramOfStudy;
using Pdc.Domain.Interfaces.UseCases.Role;
using Pdc.Domain.Interfaces.UseCases.User;
using Pdc.Domain.Interfaces.UseCases.Version;
using System.Reflection;

namespace Pdc.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        // ProgramOfStudy
        services.AddScoped<ICreateProgramOfStudyUseCase, AddProgramOfStudy>();
        services.AddScoped<IDeleteProgramOfStudyUseCase, DeleteProgramOfStudy>();
        services.AddScoped<IGetProgramOfStudiesUseCase, GetProgramOfStudies>();
        services.AddScoped<IUpdateProgramOfStudyUseCase, UpdateProgramOfStudy>();
        services.AddScoped<IGetProgramOfStudyUseCase, GetProgramOfStudy>();
        //Competency
        services.AddScoped<ICreateCompetencyUseCase, AddCompetency>();
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
        // ComplementaryInformation
        services.AddScoped<IGetComplementaryInformationUseCase, GetComplementaryInformation>();
        services.AddScoped<IUpdateComplementaryInformationUseCase, UpdateComplementaryInformation>();
        services.AddScoped<IDeleteComplementaryInformationUseCase, DeleteComplementaryInformation>();
        services.AddScoped<IAddComplementaryInformationUseCase, AddComplementaryInformation>();

        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}