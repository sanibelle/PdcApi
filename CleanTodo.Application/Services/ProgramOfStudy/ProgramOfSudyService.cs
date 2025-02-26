using Pdc.Application.DTOS;
using Pdc.Application.Exceptions;
using Pdc.Domain.Interfaces.Repositories;

namespace Pdc.Application.Service.Todo;


public class ProgramOfSudyService : IProgramOfSudyService
{
    private readonly IProgramOfStudyRespository _programOfStudyRespository;

    public ProgramOfSudyService(IProgramOfStudyRespository programOfStudyRespository)
    {
        _programOfStudyRespository = programOfStudyRespository;
    }

    public async Task<ProgramOfStudyDto> FindById(Guid id)
    {
        var programOfStudy = await _programOfStudyRespository.FindById(id);
        if (programOfStudy == null)
        {
            throw new NotFoundException();
        }
        //TODO automapper
        return new ProgramOfStudyDto
        {
            Id = programOfStudy.Id,
            Code = programOfStudy.Code,
            Name = programOfStudy.Name,
            Sanction = programOfStudy.Sanction,
            MonthsDuration = programOfStudy.MonthsDuration,
            SpecificDurationHours = programOfStudy.SpecificDurationHours,
            TotalDurationHours = programOfStudy.TotalDurationHours,
            PublishedOn = programOfStudy.PublishedOn,
            Competencies = programOfStudy.Competencies
        };
    }

    public async Task Delete(Guid id)
    {
        await _programOfStudyRespository.Delete(id);
    }

    public async Task<IList<ProgramOfStudyDto>> GetAll()
    {
        var programsOfStudy = await _programOfStudyRespository.GetAll();
        //TODO automapper
        return programsOfStudy.Select(x => new ProgramOfStudyDto
        {
            Id = x.Id,
            Code = x.Code,
            Name = x.Name,
            Sanction = x.Sanction,
            MonthsDuration = x.MonthsDuration,
            SpecificDurationHours = x.SpecificDurationHours,
            TotalDurationHours = x.TotalDurationHours,
            PublishedOn = x.PublishedOn,
            Competencies = x.Competencies
        }).ToList();
    }
}
