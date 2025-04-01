using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Application.Exceptions;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;

namespace Pdc.Application.UseCase;

public class GetProgramOfStudy : IGetProgramOfStudyUseCase
{
    private readonly IProgramOfStudyRespository _programOfStudyRespository;
    private readonly IMapper _mapper;

    public GetProgramOfStudy(IProgramOfStudyRespository programOfStudyRespository, IMapper mapper)
    {
        _programOfStudyRespository = programOfStudyRespository;
        _mapper = mapper;
    }

    public async Task<ProgramOfStudyDTO> Execute(string code)
    {
        try
        {
            ProgramOfStudy program = await _programOfStudyRespository.FindByCode(code);
            return _mapper.Map<ProgramOfStudyDTO>(program);
        }
        catch (EntityNotFoundException)
        {
            throw new NotFoundException();
        }
    }
}
