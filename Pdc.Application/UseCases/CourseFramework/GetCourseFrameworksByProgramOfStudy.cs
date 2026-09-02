using AutoMapper;
using Pdc.Application.DTOS.CourseFramework;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.CourseFramework;
using Pdc.Domain.Models.CourseFramework;

namespace Pdc.Application.UseCases;

public class GetCourseFrameworksByProgramOfStudy : IGetCourseFrameworksByProgramOfStudyUseCase
{
    private readonly ICourseFrameworkRepository _courseFrameworksRepository;
    private readonly IMapper _mapper;

    public GetCourseFrameworksByProgramOfStudy(ICourseFrameworkRepository courseFrameworksRepository, IMapper mapper)
    {
        _courseFrameworksRepository = courseFrameworksRepository;
        _mapper = mapper;
    }

    public async Task<IList<UnTrackedCourseFrameworkDTO>> Execute(string programOfStudyCode)
    {
        IList<CourseFramework> programs = await _courseFrameworksRepository.GetByProgramOfStudy(programOfStudyCode);
        return _mapper.Map<IList<UnTrackedCourseFrameworkDTO>>(programs);
    }
}
