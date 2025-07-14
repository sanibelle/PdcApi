using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.MinisterialSpecification;

namespace Pdc.Application.UseCase;

public class GetCompetenciesByProgramOfStudy : IGetCompetenciesByProgramOfStudyUseCase
{
    private readonly ICompetencyRespository _competencyRespository;
    private readonly IMapper _mapper;

    public GetCompetenciesByProgramOfStudy(ICompetencyRespository programOfStudyRespository, IMapper mapper)
    {
        _competencyRespository = programOfStudyRespository;
        _mapper = mapper;
    }

    public async Task<IList<CompetencyDTO>> Execute(string programOfStudyCode)
    {
        List<MinisterialCompetency> programs = await _competencyRespository.GetByProgramOfStudy(programOfStudyCode);
        return _mapper.Map<IList<CompetencyDTO>>(programs);
    }
}
