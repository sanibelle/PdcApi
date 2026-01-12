using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.ProgramOfStudy;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Infrastructure.Exceptions;

namespace Pdc.Application.UseCases;

public class GetProgramOfStudy : IGetProgramOfStudyUseCase
{
    private readonly IProgramOfStudyRepository _programOfStudyRepository;
    private readonly IMapper _mapper;

    public GetProgramOfStudy(IProgramOfStudyRepository programOfStudyRepository, IMapper mapper)
    {
        _programOfStudyRepository = programOfStudyRepository;
        _mapper = mapper;
    }

    public async Task<ProgramOfStudyDTO> Execute(string code)
    {
        try
        {
            ProgramOfStudy program = await _programOfStudyRepository.FindByCode(code);
            return _mapper.Map<ProgramOfStudyDTO>(program);
        }
        catch (EntityNotFoundException)
        {
            throw new NotFoundException();
        }
    }
}
