using AutoMapper;
using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.Identity;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using Pdc.Infrastructure.Entities.Version;

namespace Pdc.Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // common

        CreateMap<Competency, CompetencyEntity>()
            .ForMember(x => x.RealisationContexts, opt => opt.Ignore())
            .ForMember(x => x.CompetencyElements, opt => opt.Ignore())
            .PreserveReferences()
            .ReverseMap()
            .ForMember(dest => dest.RealisationContexts,  // explicitly map back
                opt => opt.MapFrom(src => src.RealisationContexts))
            .ForMember(dest => dest.Units,
                opt => opt.MapFrom((src, dest, member, ctx) =>
                    src.Units == null ? null : ctx.Mapper.Map<Units>(src.Units)));

        CreateMap<Units, UnitsEntity>()
            .PreserveReferences()
            .ReverseMap()
            .PreserveReferences();

        CreateMap<Changeable, ChangeableEntity>()
            .PreserveReferences()
            .ReverseMap()
            .PreserveReferences();

        CreateMap<PerformanceCriteria, PerformanceCriteriaEntity>()
            .PreserveReferences()
            .ForMember(x => x.ComplementaryInformations, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.ComplementaryInformations,  // explicitly map back
                opt => opt.MapFrom(src => src.ComplementaryInformations))
            .PreserveReferences();

        CreateMap<ComplementaryInformation, ComplementaryInformationEntity>()
            .ForMember(dest => dest.CreatedById,
               opt => opt.MapFrom(src => src.CreatedBy != null ? src.CreatedBy.Id : null))
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Changeable, opt => opt.Ignore())
            .ForMember(dest => dest.ChangeRecord, opt => opt.Ignore())
            .ForMember(dest => dest.ChangeRecordId, opt => opt.Ignore())
            .PreserveReferences()
            .ReverseMap()
            .PreserveReferences()
            .ForMember(dest => dest.CreatedBy,
                opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.WrittenOnChangeRecord,
               opt => opt.MapFrom(src => src.ChangeRecord));

        CreateMap<ContentElement, ContentElementEntity>()
            .PreserveReferences()
            .ForMember(x => x.ComplementaryInformations, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.ComplementaryInformations,  // explicitly map back
                opt => opt.MapFrom(src => src.ComplementaryInformations))
            .PreserveReferences();

        CreateMap<RealisationContext, RealisationContextEntity>()
            .PreserveReferences()
            .ForMember(x => x.ComplementaryInformations, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.ComplementaryInformations,  // explicitly map back
                opt => opt.MapFrom(src => src.ComplementaryInformations))
            .PreserveReferences();

        CreateMap<ChangeRecord, ChangeRecordEntity>()
            .PreserveReferences()
            .ForMember(dest => dest.CreatedById,

               opt => opt.MapFrom(src => src.CreatedBy != null ? src.CreatedBy.Id : null))
            .ForMember(dest => dest.CreatedBy,
                opt => opt.Ignore()) // will not track and only use the id to prevent ef core trying to create a new user.
            .ForMember(dest => dest.ValidatedById,

               opt => opt.MapFrom(src => src.ValidatedBy != null ? src.ValidatedBy.Id : null))
            .ForMember(dest => dest.ValidatedBy,
                opt => opt.Ignore())

            .ForMember(dest => dest.NextChangeRecordId,
                opt => opt.MapFrom(src => src.NextChangeRecord == null ? null : src.NextChangeRecord.Id))
            .ForMember(dest => dest.NextChangeRecord,
                opt => opt.Ignore())

            .ForMember(dest => dest.ParentChangeRecordId,
                opt => opt.MapFrom(src => src.ParentChangeRecord == null ? null : src.ParentChangeRecord.Id))
            .ForMember(dest => dest.ParentChangeRecord,  // explicitly map back
                opt => opt.Ignore())
            .ReverseMap();

        // security
        CreateMap<User, IdentityUserEntity>()
            .PreserveReferences() // used for the change record references
            .ReverseMap()
            .PreserveReferences();

        // Ministerial
        CreateMap<ProgramOfStudy, ProgramOfStudyEntity>()
            .ForMember(x => x.Competencies, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Competencies, opt => opt.MapFrom(src => src.Competencies));

        CreateMap<MinisterialCompetency, CompetencyEntity>()
            .ForMember(x => x.RealisationContexts, opt => opt.Ignore())
            .ForMember(x => x.CompetencyElements, opt => opt.Ignore())
            .ForMember(x => x.ChangeRecord, opt => opt.Ignore())
            .PreserveReferences();

        // Do not remove, the usage of explicit mapping instead of using reverseMap is to prevent the creation of the empty Units object when the Units property is null.
        CreateMap<CompetencyEntity, MinisterialCompetency>()
            .ForMember(dest => dest.CompetencyElements,  // explicitly map back
                opt => opt.MapFrom(src => src.CompetencyElements))
            .ForMember(dest => dest.Units,
                opt => opt.MapFrom((src, dest, member, ctx) =>
                    src.Units == null ? null : ctx.Mapper.Map<Units>(src.Units)));

        CreateMap<MinisterialCompetencyElement, CompetencyElementEntity>()
            .PreserveReferences() // used for the ChangeRecordEntity references
            .ForMember(x => x.ComplementaryInformations, opt => opt.Ignore())
            .ForMember(x => x.PerformanceCriterias, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.PerformanceCriterias,  // explicitly map back
                opt => opt.MapFrom(src => src.PerformanceCriterias))
            .ForMember(dest => dest.ComplementaryInformations,  // explicitly map back
                opt => opt.MapFrom(src => src.ComplementaryInformations))
            .PreserveReferences(); // used for the change record references

        // CourseFrameworkCompetency
        CreateMap<CourseFrameworkCompetency, CourseFrameworkCompetencyEntity>()
            .PreserveReferences() // used for the change record references
            .ReverseMap();

        CreateMap<CourseFrameworkCompetencyElement, CourseFrameworkCompetencyElementEntity>()
            .PreserveReferences() // used for the change record references
            .ReverseMap()
            .PreserveReferences(); // used for the change record references;

        CreateMap<CompetencyElement, CompetencyElementEntity>()
            .PreserveReferences() // used for the change record references
            .ReverseMap()
            .PreserveReferences(); // used for the change record references;
    }
}
