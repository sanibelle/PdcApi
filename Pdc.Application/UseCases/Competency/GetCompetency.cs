using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Infrastructure.Exceptions;

namespace Pdc.Application.UseCases;

public class GetCompetency : IGetCompetencyUseCase
{
    private readonly ICompetencyRepository _competencyRepository;
    private readonly IMapper _mapper;

    public GetCompetency(ICompetencyRepository competencyRepository, IMapper mapper)
    {
        _competencyRepository = competencyRepository;
        _mapper = mapper;
    }

    public async Task<CompetencyDTO> Execute(string programOfStudyCode, string competencyCode)
    {
        try
        {
            MinisterialCompetency competency = await _competencyRepository.FindByCode(competencyCode);
            return _mapper.Map<CompetencyDTO>(competency);
        }
        catch (EntityNotFoundException)
        {
            throw new NotFoundException();
        }
    }
}
