using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Competency;
using Pdc.Domain.Models.MinisterialSpecification;

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
            MinisterialCompetency competency = await _competencyRepository.FindByCode(competencyCode);
            return _mapper.Map<CompetencyDTO>(competency);
    }
}
