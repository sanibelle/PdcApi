using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Pdc.Application.DTOS;
using Pdc.Domain.Entities.MinisterialSpecification;
using Pdc.Domain.Interfaces.Repositories;

namespace Pdc.Application.UseCase;

public class CreateCompetency : ICreateCompetencyUseCase
{
    private readonly IValidator<CreateCompetencyDTO> _validator;
    private readonly ICompetencyRespository _competencyRepository;
    private readonly IMapper _mapper;

    public CreateCompetency(ICompetencyRespository competencyRepository, IMapper mapper, IValidator<CreateCompetencyDTO> validator)
    {
        _competencyRepository = competencyRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<CompetencyDTO> Execute(string studyProgramCode, CreateCompetencyDTO createCompetencyDto)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(createCompetencyDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        MinisterialCompetency programOfStudy = _mapper.Map<MinisterialCompetency>(createCompetencyDto);
        MinisterialCompetency savedCompetency = await _competencyRepository.Add(programOfStudy);

        return _mapper.Map<CompetencyDTO>(savedCompetency);
    }
}