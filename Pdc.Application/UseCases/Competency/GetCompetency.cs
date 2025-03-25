using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Application.Exceptions;
using Pdc.Domain.Entities.MinisterialSpecification;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;

namespace Pdc.Application.UseCase;

public class GetCompetency : IGetCompetencyUseCase
{
    private readonly ICompetencyRespository _programOfStudyRespository;
    private readonly IMapper _mapper;

    public GetCompetency(ICompetencyRespository programOfStudyRespository, IMapper mapper)
    {
        _programOfStudyRespository = programOfStudyRespository;
        _mapper = mapper;
    }

    public async Task<CompetencyDTO> Execute(string code)
    {
        try
        {
            Competency program = await _programOfStudyRespository.FindByCode(code);
            return _mapper.Map<CompetencyDTO>(program);
        }
        catch (EntityNotFoundException)
        {
            throw new NotFoundException();
        }
    }
}
