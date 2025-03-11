using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Domain.Entities.CourseFramework;
using Pdc.Domain.Interfaces.Repositories;

namespace Pdc.Application.UseCase;

public class CreateProgramOfSudy : ICreateProgramOfStudyUseCase
{
    private readonly IProgramOfStudyRespository _programOfStudyRespository;
    private readonly IMapper _mapper;

    public CreateProgramOfSudy(IProgramOfStudyRespository programOfStudyRespository, IMapper mapper)
    {
        _programOfStudyRespository = programOfStudyRespository;
        _mapper = mapper;

    }

    public async Task<ProgramOfStudyDTO> Execute(CreateProgramOfStudyDTO createProgramOfStudyDto)
    {

        ProgramOfStudy programOfStudy = _mapper.Map<ProgramOfStudy>(createProgramOfStudyDto);

        ProgramOfStudy savedProgramOfStudy = await _programOfStudyRespository.Add(programOfStudy);

        return _mapper.Map<ProgramOfStudyDTO>(savedProgramOfStudy);
    }
}