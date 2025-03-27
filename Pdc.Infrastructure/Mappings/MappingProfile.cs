using AutoMapper;
using Pdc.Domain.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.CourseFramework;

namespace Pdc.Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ProgramOfStudy
        CreateMap<ProgramOfStudy, ProgramOfStudyEntity>().ReverseMap();
    }
}