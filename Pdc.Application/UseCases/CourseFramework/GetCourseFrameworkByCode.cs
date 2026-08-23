using AutoMapper;
using Pdc.Application.DTOS.CourseFramework;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.CourseFramework;

namespace Pdc.Application.UseCases;

public class GetCourseFrameworkByCode(ICourseFrameworkRepository courseFrameworksRepository, IMapper mapper) : IGetCourseFrameworkByCodeUseCase
{
    public async Task<UntrackedCourseFrameworkDTO> Execute(string programOfStudyCode)
    {
        var programs = await courseFrameworksRepository.FindByCode(programOfStudyCode);
        return mapper.Map<UntrackedCourseFrameworkDTO>(programs);
    }
}
