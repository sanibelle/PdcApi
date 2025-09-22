using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Domain.Interfaces.Repositories;

namespace Pdc.Application.UseCases;

public class GetProgramOfStudies : IGetProgramOfStudiesUseCase
{
    private readonly IProgramOfStudyRepository _programOfStudyRepository;
    private readonly IMapper _mapper;

    public GetProgramOfStudies(IProgramOfStudyRepository programOfStudyRepository, IMapper mapper)
    {
        _programOfStudyRepository = programOfStudyRepository;
        _mapper = mapper;
    }

    public async Task<IList<ProgramOfStudyDTO>> Execute()
    {
        var programs = await _programOfStudyRepository.GetAll();
        return _mapper.Map<IList<ProgramOfStudyDTO>>(programs);
    }
}
