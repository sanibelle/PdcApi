using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Application.Exceptions;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;

namespace Pdc.Application.UseCase;

public class GetCompetency : IGetCompetencyUseCase
{
    private readonly ICompetencyRespository _competencyRespository;
    private readonly IMapper _mapper;

    public GetCompetency(ICompetencyRespository competencyRespository, IMapper mapper)
    {
        _competencyRespository = competencyRespository;
        _mapper = mapper;
    }

    public async Task<CompetencyDTO> Execute(string programOfStudyCode, string competencyCode)
    {
        try
        {
            MinisterialCompetency competency = await _competencyRespository.FindByCode(programOfStudyCode, competencyCode);
            return _mapper.Map<CompetencyDTO>(competency);
        }
        catch (EntityNotFoundException)
        {
            throw new NotFoundException();
        }
    }
}
