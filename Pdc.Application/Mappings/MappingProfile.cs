using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Application.DTOS.Common;
using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;

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

    }
}