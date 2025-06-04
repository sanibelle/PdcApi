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
        CreateMap<CompetencyDTO, MinisterialCompetency>().ReverseMap();
        CreateMap<CompetencyElementDTO, MinisterialCompetencyElement>().ReverseMap();
        CreateMap<ChangeableDTO, RealisationContext>().ReverseMap();
        CreateMap<ChangeableDTO, PerformanceCriteria>().ReverseMap();
        CreateMap<ChangeRecordDTO, ChangeRecord>().ReverseMap();
        CreateMap<UserDTO, User>().ReverseMap();
        CreateMap<ComplementaryInformationDTO, ComplementaryInformation>()
        .PreserveReferences()
        .ReverseMap()
        .ForMember(dest => dest.WrittenOnVersion, opt => opt.MapFrom(src => src.WrittenOnVersion != null ? src.WrittenOnVersion.VersionNumber : default));
    }
}
