using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Application.DTOS.Common;
using Pdc.Domain.DTOS.Common;
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
            .ForMember(dest => dest.ChangeRecord, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.IsDraft, opt => opt.MapFrom(src => src.ChangeRecord != null ? src.ChangeRecord.IsDraft : default))
            .ForMember(dest => dest.ChangeRecordId, opt => opt.MapFrom(src => src.ChangeRecord != null ? src.ChangeRecord.Id : default))
            .ForMember(dest => dest.ChangeRecordNumber, opt => opt.MapFrom(src => src.ChangeRecord != null ? src.ChangeRecord.ChangeRecordNumber : (int?)null));
        CreateMap<CompetencyElementDTO, MinisterialCompetencyElement>().ReverseMap();
        CreateMap<ChangeableDTO, RealisationContext>().ReverseMap();
        CreateMap<ChangeableDTO, PerformanceCriteria>().ReverseMap();
        CreateMap<ChangeableDTO, Changeable>().ReverseMap();
        CreateMap<ChangeRecordDTO, ChangeRecord>().ReverseMap();
        CreateMap<ChangeDetailDTO, ChangeDetail>()
            .ReverseMap()
            .ForMember(dest => dest.ChangeableId, opt => opt.MapFrom(src => src.Changeable.Id != null ? src.Changeable.Id.Value : default));
        CreateMap<UserDTO, User>()
            .ReverseMap();
        CreateMap<ComplementaryInformationDTO, ComplementaryInformation>()
        .ForMember(dest => dest.CreatedBy, opt =>
        {
            opt.Condition(src => !string.IsNullOrEmpty(src.CreatedBy?.UserName)); // Only map if source has CreatedBy
            opt.MapFrom(src => src.CreatedBy);
        })
        .ForMember(dest => dest.WrittenOnChangeRecord, opt => opt.Ignore())
        .PreserveReferences()
        .ReverseMap()
        .ForMember(dest => dest.ChangeRecordNumber, opt => opt.MapFrom(src => src.WrittenOnChangeRecord != null ? src.WrittenOnChangeRecord.ChangeRecordNumber : (int?)default));
    }
}
