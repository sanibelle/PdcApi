using AutoMapper;
using Pdc.Application.DTOS.CourseFramework;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.CourseFramework;

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

    public async Task<IList<UntrackedCourseFrameworkDTO>> Execute(string programOfStudyCode)
    {
        var programs = await _courseFrameworksRepository.GetByProgramOfStudy(programOfStudyCode);
        return _mapper.Map<IList<UntrackedCourseFrameworkDTO>>(programs);
    }
}
