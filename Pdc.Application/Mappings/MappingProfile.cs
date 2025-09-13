using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Application.DTOS.Common;
using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ProgramOfStudy  
        CreateMap<ProgramOfStudy, ProgramOfStudyDTO>().ReverseMap();
        // Comptency  
        CreateMap<CompetencyDTO, MinisterialCompetency>()
            .ForMember(dest => dest.CurrentVersion, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.IsDraft, opt => opt.MapFrom(src => src.CurrentVersion != null ? src.CurrentVersion.IsDraft : default))
            .ForMember(dest => dest.VersionId, opt => opt.MapFrom(src => src.CurrentVersion != null ? src.CurrentVersion.Id : default))
            .ForMember(dest => dest.VersionNumber, opt => opt.MapFrom(src => src.CurrentVersion != null ? src.CurrentVersion.VersionNumber : (int?)null));
        CreateMap<CompetencyElementDTO, MinisterialCompetencyElement>().ReverseMap();
        CreateMap<ChangeableDTO, RealisationContext>().ReverseMap();
        CreateMap<ChangeableDTO, PerformanceCriteria>().ReverseMap();
        CreateMap<ChangeRecordDTO, ChangeRecord>().ReverseMap();
        CreateMap<UserDTO, User>()
            .ReverseMap();
        CreateMap<ComplementaryInformationDTO, ComplementaryInformation>()
        .ForMember(dest => dest.CreatedBy, opt =>
        {
            opt.Condition(src => !string.IsNullOrEmpty(src.CreatedBy?.DisplayName)); // Only map if source has CreatedBy
            opt.MapFrom(src => src.CreatedBy);
        })
        .ForMember(dest => dest.WrittenOnVersion, opt => opt.Ignore())
        .PreserveReferences()
        .ReverseMap()
        .ForMember(dest => dest.WrittenOnVersion, opt => opt.MapFrom(src => src.WrittenOnVersion != null ? src.WrittenOnVersion.VersionNumber : (int?)default));
    }
}
