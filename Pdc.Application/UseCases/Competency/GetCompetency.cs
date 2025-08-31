using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Infrastructure.Exceptions;

namespace Pdc.Application.UseCase;

public class GetCompetency : IGetCompetencyUseCase
{
    private readonly ICompetencyRepository _competencyRespository;
    private readonly IMapper _mapper;

    public GetCompetency(ICompetencyRepository competencyRespository, IMapper mapper)
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
