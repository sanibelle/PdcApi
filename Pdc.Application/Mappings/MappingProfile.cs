using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Domain.Entities.CourseFramework;

namespace Pdc.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProgramOfStudy, ProgramOfStudyDTO>().ReverseMap();
        CreateMap<UpsertProgramOfStudyDTO, ProgramOfStudy>().ReverseMap();

    }
}