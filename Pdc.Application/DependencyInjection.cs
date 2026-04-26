using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pdc.Application.Mappings;
using Pdc.Application.Services.Competency;
using Pdc.Application.Services.UserService;
using Pdc.Application.UseCases;
using Pdc.Application.UseCases.Competency;
using Pdc.Application.UseCases.Versioning;
using Pdc.Domain.Interfaces.UseCases.Competency;
using Pdc.Domain.Interfaces.UseCases.ProgramOfStudy;
using Pdc.Domain.Interfaces.UseCases.Role;
using Pdc.Domain.Interfaces.UseCases.User;
using Pdc.Domain.Interfaces.UseCases.Versioning;
using System.Reflection;

namespace Pdc.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        // ProgramOfStudy
        services.AddScoped<IAddProgramOfStudyUseCase, AddProgramOfStudy>();
        services.AddScoped<IDeleteProgramOfStudyUseCase, DeleteProgramOfStudy>();
        services.AddScoped<IGetProgramOfStudiesUseCase, GetProgramOfStudies>();
        services.AddScoped<IUpdateProgramOfStudyUseCase, UpdateProgramOfStudy>();
        services.AddScoped<IGetProgramOfStudyUseCase, GetProgramOfStudy>();
        //Competency
        services.AddScoped<IAddCompetencyUseCase, AddCompetency>();
        services.AddScoped<IGetCompetencyUseCase, GetCompetency>();
        services.AddScoped<IGetCompetencyWithChangeDetailsUseCase, GetCompetencyWithChangeDetails>();
        services.AddScoped<IGetCompetenciesByProgramOfStudyUseCase, GetCompetenciesByProgramOfStudy>();
        services.AddScoped<IUpdateDraftV1CompetencyUseCase, UpdateDraftV1Competency>();
        services.AddScoped<IUpdatePublishedCompetencyUseCase, UpdatePublishedCompetency>();
        services.AddScoped<IDeleteCompetencyUseCase, DeleteCompetency>();
        services.AddScoped<CompetencyService>();
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
        // ChangeRecord
        services.AddScoped<IPublishChangeRecordUseCase, PublishChangeRecord>();
        //Changeable
        services.AddScoped<IUpdateChangeableUseCase, UpdateChangeable>();

        services.AddAutoMapper(cfg =>
        {
            cfg.LicenseKey = configuration["AutoMapper:LicenseKey"];
            cfg.AddMaps(typeof(MappingProfile));
        });

        return services;
    }
}