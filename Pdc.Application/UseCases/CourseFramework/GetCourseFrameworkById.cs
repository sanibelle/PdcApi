using AutoMapper;
using Pdc.Application.DTOS.CourseFramework;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.CourseFramework;

namespace Pdc.Application.UseCases;

public class GetCourseFrameworkById(ICourseFrameworkRepository courseFrameworksRepository, IMapper mapper) : IGetCourseFrameworkByIdUseCase
{
    public async Task<TrackedCourseFrameworkDTO> Execute(Guid id)
    {
        var programs = await courseFrameworksRepository.FindById(id);
        return mapper.Map<TrackedCourseFrameworkDTO>(programs);
    }
}
