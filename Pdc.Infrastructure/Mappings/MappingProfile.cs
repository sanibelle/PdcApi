using AutoMapper;
using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // common
        CreateMap<Competency, CompetencyEntity>()
            .ForMember(dest => dest.CurrentVersion, opt => opt.MapFrom<ChangeRecordResolver>())
            .ReverseMap();
        CreateMap<ChangeableEntity, AChangeable>()
            .ReverseMap();
        CreateMap<PerformanceCriteriaDTO, PerformanceCriteriaEntity>()
            .ReverseMap();
        CreateMap<ComplementaryInformation, ComplementaryInformationEntity>()
            .ForMember(dest => dest.WrittenOnVersion, opt => opt.MapFrom<ChangeRecordResolver>())
            .ReverseMap();
        CreateMap<CompetencyElement, CompetencyElementEntity>()
            .ReverseMap();
        CreateMap<ContentElement, ContentElementEntity>()
            .ReverseMap();
        CreateMap<RealisationContextEntity, RealisationContext>()
            .ReverseMap();
        CreateMap<ChangeRecord, ChangeRecordEntity>()
            .ReverseMap();

        // Ministerial
        CreateMap<ProgramOfStudy, ProgramOfStudyEntity>()
            .ForMember(dest => dest.Competencies, opt => opt.MapFrom(src => src.Competencies))
            .ReverseMap()
            .ForMember(dest => dest.Competencies, opt => opt.MapFrom(src => src.Competencies));

        CreateMap<MinisterialCompetency, CompetencyEntity>()
            .ForMember(dest => dest.CurrentVersion, opt => opt.MapFrom<ChangeRecordResolver>())
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
