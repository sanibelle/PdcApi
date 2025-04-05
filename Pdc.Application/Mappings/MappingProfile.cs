using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Application.DTOS.Common;
using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;
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
        CreateMap<ChangeableDTO, PerformanceCriteriaDTO>().ReverseMap();
        CreateMap<ChangeRecordDTO, ChangeRecord>().ReverseMap();
        CreateMap<ComplementaryInformationDTO, ComplementaryInformation>()
        .ForMember(dest => dest.WrittenOnVersion, opt => opt.MapFrom(src => new ChangeRecord(src.WrittenOnVersion ?? -1)))
        .ReverseMap()
        .ForMember(dest => dest.WrittenOnVersion, opt => opt.MapFrom(src => src.WrittenOnVersion.VersionNumber));
    }
}
