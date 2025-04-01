using AutoMapper;
using FluentValidation;
using Pdc.Application.DTOS;
using Pdc.Domain.Interfaces.Repositories;


namespace Pdc.Application.UseCase;

public class UpdateCompetency : IUpdateCompetencyUseCase
{
    private readonly IValidator<CompetencyDTO> _validator;
    private readonly ICompetencyRespository _programOfStudyRespository;
    private readonly IMapper _mapper;

    public UpdateCompetency(ICompetencyRespository programOfStudyRespository,
                               IMapper mapper,
                               IValidator<CompetencyDTO> validator)
    {
        _programOfStudyRespository = programOfStudyRespository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<CompetencyDTO> Execute(string programOfStudyCode, CompetencyDTO updateCompetencyDto)
    {
        var validationResult = await _validator.ValidateAsync(updateCompetencyDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        throw new NotImplementedException();
        //competency existingCompetency = await _programOfStudyRespository.FindById(programOfStudyId);
        //_mapper.Map(updateCompetencyDto, existingCompetency);
        //competency updatedCompetency = await _programOfStudyRespository.Update(existingCompetency);
        //return _mapper.Map<CompetencyDTO>(updatedCompetency);
    }
}