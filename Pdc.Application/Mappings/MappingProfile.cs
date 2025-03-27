using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Domain.Entities.CourseFramework;
using Pdc.Domain.Entities.MinisterialSpecification;

namespace Pdc.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ProgramOfStudy
        CreateMap<ProgramOfStudy, ProgramOfStudyDTO>().ReverseMap();
        CreateMap<CreateProgramOfStudyDTO, ProgramOfStudy>().ReverseMap();
        // Comptency
        CreateMap<CompetencyDTO, MinisterialCompetency>().ReverseMap();
        CreateMap<CreateCompetencyDTO, MinisterialCompetency>().ReverseMap();

    }
}