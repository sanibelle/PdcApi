using AutoMapper;
using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;
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
