using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.MinisterialSpecification;

namespace Pdc.Application.UseCases;

public class GetCompetenciesByProgramOfStudy : IGetCompetenciesByProgramOfStudyUseCase
{
    private readonly ICompetencyRepository _competencyRepository;
    private readonly IMapper _mapper;

    public GetCompetenciesByProgramOfStudy(ICompetencyRepository programOfStudyRepository, IMapper mapper)
    {
        _competencyRepository = programOfStudyRepository;
        _mapper = mapper;
    }

    public async Task<IList<CompetencyDTO>> Execute(string programOfStudyCode)
    {
        List<MinisterialCompetency> programs = await _competencyRepository.GetByProgramOfStudy(programOfStudyCode);
        return _mapper.Map<IList<CompetencyDTO>>(programs);
    }
}
