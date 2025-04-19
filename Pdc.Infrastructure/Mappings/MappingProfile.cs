using AutoMapper;
using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.Identity;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // common
        CreateMap<Competency, CompetencyEntity>()
            .ReverseMap();
        CreateMap<ChangeableEntity, AChangeable>()
            .ReverseMap();
        CreateMap<PerformanceCriteria, PerformanceCriteriaEntity>()
            .ReverseMap();
        CreateMap<ComplementaryInformation, ComplementaryInformationEntity>()
            .ReverseMap();
        CreateMap<CompetencyElement, CompetencyElementEntity>()
            .ReverseMap();
        CreateMap<ContentElement, ContentElementEntity>()
            .ReverseMap();
        CreateMap<RealisationContextEntity, RealisationContext>()
            .ReverseMap();
        CreateMap<ChangeRecord, ChangeRecordEntity>()
            .ReverseMap();

        // security
        CreateMap<User, IdentityUserEntity>()
            .PreserveReferences() // used for the versions references
            .ReverseMap();

        // Ministerial
        CreateMap<ProgramOfStudy, ProgramOfStudyEntity>()
            .ForMember(dest => dest.Competencies, opt => opt.MapFrom(src => src.Competencies))
            .ReverseMap()
            .ForMember(dest => dest.Competencies, opt => opt.MapFrom(src => src.Competencies));

        CreateMap<MinisterialCompetency, CompetencyEntity>()
            .PreserveReferences() // used for the versions references
            .ReverseMap();
        CreateMap<MinisterialCompetencyElement, CompetencyElementEntity>()
            .ReverseMap();

        // CourseFrameworkCompetency
        CreateMap<CourseFrameworkCompetency, CourseFrameworkCompetencyEntity>()
            .ReverseMap();

        CreateMap<CourseFrameworkCompetencyElement, CourseFrameworkCompetencyElementEntity>()
            .ReverseMap();

        CreateMap<CompetencyElement, CompetencyElementEntity>()
            .ReverseMap();
    }
}
