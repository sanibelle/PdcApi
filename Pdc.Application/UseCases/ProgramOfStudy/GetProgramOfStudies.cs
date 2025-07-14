using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Domain.Interfaces.Repositories;

namespace Pdc.Application.UseCase;

public class GetProgramOfStudies : IGetProgramOfStudiesUseCase
{
    private readonly IProgramOfStudyRespository _programOfStudyRespository;
    private readonly IMapper _mapper;

    public GetProgramOfStudies(IProgramOfStudyRespository programOfStudyRespository, IMapper mapper)
    {
        _programOfStudyRespository = programOfStudyRespository;
        _mapper = mapper;
    }

    public async Task<IList<ProgramOfStudyDTO>> Execute()
    {
        var programs = await _programOfStudyRespository.GetAll();
        return _mapper.Map<IList<ProgramOfStudyDTO>>(programs);
    }
}
