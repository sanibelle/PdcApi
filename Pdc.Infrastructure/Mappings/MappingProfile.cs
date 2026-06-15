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
            .ForMember(dest => dest.ProgramOfStudy, opt => opt.Ignore())
            .ForMember(dest => dest.CompetencyElements, opt => opt.Ignore())
            .PreserveReferences()
            .ReverseMap()
            .ForMember(dest => dest.RealisationContexts,
                opt => opt.MapFrom(src => src.RealisationContexts))
            .ForMember(dest => dest.Units,
                opt => opt.MapFrom(src => src.Units));

        CreateMap<Units, UnitsEntity>()
            .PreserveReferences()
            .ReverseMap()
            .PreserveReferences();

        CreateMap<ChangeDetail, ChangeDetailEntity>()
            .ForMember(dest => dest.ChangeRecordId,
               opt => opt.MapFrom(src => src.ChangeRecord != null ? src.ChangeRecord.Id : null))
            .PreserveReferences()
            .ReverseMap()
            .PreserveReferences();

        CreateMap<Changeable, ChangeableEntity>()
            .PreserveReferences()
            .ReverseMap()
            .PreserveReferences();

        CreateMap<PerformanceCriteria, PerformanceCriteriaEntity>()
            .ForMember(x => x.CompetencyElement, opt => opt.Ignore())
            .PreserveReferences()
            .ForMember(x => x.ComplementaryInformations, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.ComplementaryInformations,
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
            .ForMember(x => x.CourseFrameworkPerformanceCriteria, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.ComplementaryInformations,
                opt => opt.MapFrom(src => src.ComplementaryInformations))
            .PreserveReferences();

        CreateMap<RealisationContext, RealisationContextEntity>()
            .PreserveReferences()
            .ForMember(x => x.ComplementaryInformations, opt => opt.Ignore())
            .ForMember(x => x.Competency, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.ComplementaryInformations,
                opt => opt.MapFrom(src => src.ComplementaryInformations))
            .PreserveReferences();

        CreateMap<ChangeRecord, ChangeRecordEntity>()
            .PreserveReferences()
            .ForMember(dest => dest.ComplementaryInformations, opt => opt.Ignore())
            .ForMember(dest => dest.ChangeDetails, opt => opt.Ignore())
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
            .ForMember(dest => dest.ParentChangeRecord,
                opt => opt.Ignore());

        CreateMap<ChangeRecordEntity, ChangeRecord>()
            .MaxDepth(1)
            .PreserveReferences();

        // security
        CreateMap<User, IdentityUserEntity>()
            .PreserveReferences()
            .ForMember(x => x.NormalizedUserName, opt => opt.Ignore())
            .ForMember(x => x.NormalizedEmail, opt => opt.Ignore())
            .ForMember(x => x.EmailConfirmed, opt => opt.Ignore())
            .ForMember(x => x.PasswordHash, opt => opt.Ignore())
            .ForMember(x => x.SecurityStamp, opt => opt.Ignore())
            .ForMember(x => x.ConcurrencyStamp, opt => opt.Ignore())
            .ForMember(x => x.PhoneNumber, opt => opt.Ignore())
            .ForMember(x => x.PhoneNumberConfirmed, opt => opt.Ignore())
            .ForMember(x => x.TwoFactorEnabled, opt => opt.Ignore())
            .ForMember(x => x.LockoutEnd, opt => opt.Ignore())
            .ForMember(x => x.LockoutEnabled, opt => opt.Ignore())
            .ForMember(x => x.AccessFailedCount, opt => opt.Ignore())
            .ReverseMap()
            .PreserveReferences();

        // Ministerial
        CreateMap<ProgramOfStudy, ProgramOfStudyEntity>()
            .PreserveReferences()
            .ForMember(x => x.Competencies, opt => opt.Ignore())
            .ReverseMap()
            .PreserveReferences()
            .ForMember(dest => dest.Competencies, opt => opt.MapFrom(src => src.Competencies));

        CreateMap<MinisterialCompetency, CompetencyEntity>()
            .ForMember(x => x.RealisationContexts, opt => opt.Ignore())
            .ForMember(x => x.CompetencyElements, opt => opt.Ignore())
            .ForMember(x => x.ChangeRecord, opt => opt.Ignore())
            .ForMember(x => x.ProgramOfStudy, opt => opt.Ignore())
            .PreserveReferences();

        CreateMap<CompetencyEntity, MinisterialCompetency>()
            .PreserveReferences()
            .ForMember(dest => dest.CompetencyElements,
                opt => opt.MapFrom(src => src.CompetencyElements))
            .ForMember(dest => dest.Units,
                opt => opt.MapFrom(src => src.Units));

        CreateMap<MinisterialCompetencyElement, CompetencyElementEntity>()
            .PreserveReferences()
            .ForMember(x => x.ComplementaryInformations, opt => opt.Ignore())
            .ForMember(x => x.PerformanceCriterias, opt => opt.Ignore())
            .ForMember(x => x.Competency, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.PerformanceCriterias,
                opt => opt.MapFrom(src => src.PerformanceCriterias))
            .ForMember(dest => dest.ComplementaryInformations,
                opt => opt.MapFrom(src => src.ComplementaryInformations))
            .PreserveReferences();

        // CourseFrameworkCompetency
        CreateMap<CourseFrameworkCompetency, CourseFrameworkCompetencyEntity>()
            .PreserveReferences()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.Competency, opt => opt.Ignore())
            .ForMember(x => x.CourseFramework, opt => opt.Ignore())
            .ForMember(x => x.IsAssedElement, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<CourseFrameworkCompetencyElement, CourseFrameworkCompetencyElementEntity>()
            .PreserveReferences()
            .ForMember(x => x.CompetencyElement, opt => opt.Ignore())
            .ForMember(x => x.CourseFramework, opt => opt.Ignore())
            .ForMember(x => x.Hours, opt => opt.Ignore())
            .ReverseMap()
            .PreserveReferences();

        CreateMap<CompetencyElement, CompetencyElementEntity>()
            .PreserveReferences()
            .ForMember(x => x.PerformanceCriterias, opt => opt.Ignore())
            .ForMember(x => x.Competency, opt => opt.Ignore())
            .ReverseMap()
            .PreserveReferences();
    }
}
