using AutoMapper;
using Pdc.Domain.Entities.Common;
using Pdc.Domain.Entities.CourseFramework;
using Pdc.Domain.Entities.MinisterialSpecification;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.MinisterialSpecification;

namespace Pdc.Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // common
        CreateMap<Competency, CompetencyEntity>()
            .ReverseMap();

        CreateMap<CompetencyElement, CompetencyElementEntity>()
            .ReverseMap();

        CreateMap<ContentElement, ContentElementEntity>()
            .ReverseMap();

        CreateMap<RealisationContextEntity, RealisationContext>()
            .ReverseMap();

        // Ministerial
        CreateMap<ProgramOfStudy, ProgramOfStudyEntity>()
            .ForMember(dest => dest.Competencies, opt => opt.MapFrom(src => src.Competencies))
            .ReverseMap()
            .ForMember(dest => dest.Competencies, opt => opt.MapFrom(src => src.Competencies));

        CreateMap<MinisterialCompetency, CompetencyEntity>()
            .IncludeBase<Competency, CompetencyEntity>()
            .ReverseMap();

        // CourseFrameworkCompetency
        CreateMap<CourseFrameworkCompetency, CourseFrameworkCompetencyEntity>()
            .IncludeBase<Competency, CompetencyEntity>()
            .ReverseMap();

        CreateMap<CourseFrameworkCompetencyElement, CourseFrameworkPerformanceEntity>()
            .IncludeBase<CompetencyElement, CompetencyElementEntity>()
            .ReverseMap();

        CreateMap<CompetencyElement, CourseFrameworkPerformanceEntity>()
            .ForMember(dest => dest.CompetencyElement, opt => opt.MapFrom(src => src))
            .ReverseMap()
            .ForMember(dest => dest, opt => opt.MapFrom(src => src.CompetencyElement));

        // Migration et tests I guess! :)
    }
}
