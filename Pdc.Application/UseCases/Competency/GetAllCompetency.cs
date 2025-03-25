using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Domain.Interfaces.Repositories;

namespace Pdc.Application.UseCase;

public class GetAllCompetency : IGetAllCompetencyUseCase
{
    private readonly ICompetencyRespository _programOfStudyRespository;
    private readonly IMapper _mapper;

    public GetAllCompetency(ICompetencyRespository programOfStudyRespository, IMapper mapper)
    {
        _programOfStudyRespository = programOfStudyRespository;
        _mapper = mapper;
    }

    public async Task<IList<CompetencyDTO>> Execute()
    {
        var programs = await _programOfStudyRespository.GetAll();
        return _mapper.Map<IList<CompetencyDTO>>(programs);
    }
}
