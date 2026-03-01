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
            .PreserveReferences()
            .ReverseMap()
            .ForMember(dest => dest.Units,
                opt => opt.MapFrom((src, dest, member, ctx) =>
                    src.Units == null ? null : ctx.Mapper.Map<Units>(src.Units))); ;

        CreateMap<Units, UnitsEntity>()
            .EqualityComparison((dto, entity) => dto.Id == entity.Id)
            .PreserveReferences()
            .ReverseMap()
            .PreserveReferences();

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
            .ForMember(dest => dest.CreatedById,
               opt => opt.MapFrom(src => src.CreatedBy != null ? src.CreatedBy.Id : null))
            .ForMember(dest => dest.CreatedBy,
                opt => opt.Ignore()) // will not track and only use the id to prevent ef core trying to create a new user.
            .PreserveReferences()
            .ReverseMap()
            .PreserveReferences()
            .ForMember(dest => dest.CreatedBy,  // explicitly map back
                opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.WrittenOnVersion,
               opt => opt.MapFrom(src => src.WrittenOnVersion));

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
            .ForMember(dest => dest.CreatedById,
               opt => opt.MapFrom(src => src.CreatedBy != null ? src.CreatedBy.Id : null))
            .ForMember(dest => dest.CreatedBy,
                opt => opt.Ignore()) // will not track and only use the id to prevent ef core trying to create a new user.
            .ForMember(dest => dest.ValidatedById,
               opt => opt.MapFrom(src => src.ValidatedBy != null ? src.ValidatedBy.Id : null))
            .ForMember(dest => dest.ValidatedBy,// will not track and only use the id to prevent ef core trying to create an new user.
                opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.CreatedBy,  // explicitly map back
                opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.ValidatedBy,  // explicitly map back
                opt => opt.MapFrom(src => src.ValidatedBy))
            .ForMember(dest => dest.ParentVersion,  // explicitly map back
                opt => opt.MapFrom(src => src.ParentVersion == null ? null : src.ParentVersion))
            .ForMember(dest => dest.NextVersion,  // explicitly map back
                opt => opt.MapFrom(src => src.NextVersion == null ? null : src.NextVersion))
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
            .PreserveReferences();

        // Do not remove, the usage of explicit mapping instead of using reverseMap is to prevent the creation of the empty Units object when the Units property is null.
        CreateMap<CompetencyEntity, MinisterialCompetency>()
            .ForMember(dest => dest.Units,
                opt => opt.MapFrom((src, dest, member, ctx) =>
                    src.Units == null ? null : ctx.Mapper.Map<Units>(src.Units)));

        CreateMap<MinisterialCompetencyElement, CompetencyElementEntity>()
            .PreserveReferences() // used for the ChangeRecordEntity references
            .EqualityComparison((dto, entity) => dto.Id == entity.Id)
            .ReverseMap()
            .PreserveReferences(); // used for the versions references

        // CourseFrameworkCompetency
        CreateMap<CourseFrameworkCompetency, CourseFrameworkCompetencyEntity>()
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
