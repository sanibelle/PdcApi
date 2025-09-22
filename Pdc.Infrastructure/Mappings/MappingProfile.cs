using AutoMapper;
using AutoMapper.EquivalencyExpression;
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
            .EqualityComparison((x, y) => x.Code == y.Code)
            .ReverseMap();

        CreateMap<AChangeable, ChangeableEntity>()
            .PreserveReferences()
            .EqualityComparison((dto, entity) => dto.Id == entity.Id)
            .ReverseMap()
            .PreserveReferences();
        CreateMap<PerformanceCriteria, PerformanceCriteriaEntity>()
            .PreserveReferences()
            .EqualityComparison((dto, entity) => dto.Id == entity.Id)
            .ReverseMap()
            .PreserveReferences();

        CreateMap<ComplementaryInformation, ComplementaryInformationEntity>()
            .EqualityComparison((dto, entity) => dto.Id == entity.Id)
            .ForMember(dest => dest.CreatedBy, opt =>
            {
                opt.Condition(src => src.CreatedBy != null && src.CreatedBy.Id != null);
                opt.MapFrom(src => src.CreatedBy);
            })
            .ReverseMap();

        CreateMap<ContentElement, ContentElementEntity>()
            .PreserveReferences()
            .EqualityComparison((dto, entity) => dto.Id == entity.Id)
            .ReverseMap()
            .PreserveReferences();

        CreateMap<RealisationContext, RealisationContextEntity>()
            .PreserveReferences()
            .EqualityComparison((dto, entity) => dto.Id == entity.Id)
            .ReverseMap()
            .PreserveReferences();

        CreateMap<ChangeRecord, ChangeRecordEntity>()
            .PreserveReferences()
            .EqualityComparison((dto, entity) => dto.Id == entity.Id)
            .ReverseMap()
            .PreserveReferences();

        // security
        CreateMap<User, IdentityUserEntity>()
            .EqualityComparison((dto, entity) => dto.Id == entity.Id)
            .PreserveReferences() // used for the versions references
            .ReverseMap()
            .PreserveReferences();

        // Ministerial
        CreateMap<ProgramOfStudy, ProgramOfStudyEntity>()
            .EqualityComparison((x, y) => x.Code == y.Code)
            .ForMember(dest => dest.Competencies, opt => opt.MapFrom(src => src.Competencies))
            .ReverseMap()
            .ForMember(dest => dest.Competencies, opt => opt.MapFrom(src => src.Competencies));

        CreateMap<MinisterialCompetency, CompetencyEntity>()
            .EqualityComparison((x, y) => x.Code == y.Code)
            .PreserveReferences() // used for the ChangeRecordEntity references
            .ReverseMap()
            .PreserveReferences(); // used for the ChangeRecordEntity references;

        CreateMap<MinisterialCompetencyElement, CompetencyElementEntity>()
            .PreserveReferences() // used for the ChangeRecordEntity references
            .EqualityComparison((dto, entity) => dto.Id == entity.Id)
            .ReverseMap()
            .PreserveReferences(); // used for the versions references

        // CourseFrameworkCompetency
        CreateMap<CourseFrameworkCompetency, CourseFrameworkCompetencyEntity>()
            // TODO
            .PreserveReferences() // used for the versions references
            .ReverseMap();

        CreateMap<CourseFrameworkCompetencyElement, CourseFrameworkCompetencyElementEntity>()
            .EqualityComparison((dto, entity) => dto.Id == entity.Id)
            .PreserveReferences() // used for the versions references
            .ReverseMap()
            .PreserveReferences(); // used for the versions references;

        CreateMap<CompetencyElement, CompetencyElementEntity>()
            .EqualityComparison((dto, entity) => dto.Id == entity.Id)
            .PreserveReferences() // used for the versions references
            .ReverseMap()
            .PreserveReferences(); // used for the versions references;
    }
}
