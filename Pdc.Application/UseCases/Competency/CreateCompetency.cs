using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Pdc.Application.DTOS;
using Pdc.Application.Exceptions;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;

namespace Pdc.Application.UseCase;

public class CreateCompetency : ICreateCompetencyUseCase
{
    private readonly IValidator<CompetencyDTO> _validator;
    private readonly ICompetencyRespository _competencyRepository;
    private readonly IProgramOfStudyRespository _programOfStudyRepository;
    private readonly IMapper _mapper;

    public CreateCompetency(ICompetencyRespository competencyRepository,
                            IProgramOfStudyRespository programOfStudyRepository,
                            IMapper mapper,
                            IValidator<CompetencyDTO> validator)
    {
        _competencyRepository = competencyRepository;
        _programOfStudyRepository = programOfStudyRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<CompetencyDTO> Execute(string programOfStudyCode, CompetencyDTO createCompetencyDto)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(createCompetencyDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        ProgramOfStudy program = await _programOfStudyRepository.FindByCode(programOfStudyCode);
        await ThrowIfDuplicateCode(programOfStudyCode, createCompetencyDto.Code);

        MinisterialCompetency programOfStudy = _mapper.Map<MinisterialCompetency>(createCompetencyDto);
        MinisterialCompetency savedCompetency = await _competencyRepository.Add(program, programOfStudy);

        return _mapper.Map<CompetencyDTO>(savedCompetency);
    }

    private async Task ThrowIfDuplicateCode(string programOfStudyCode, string competencyCode)
    {
        try
        {
            var comptency = await _competencyRepository.FindByCode(programOfStudyCode, competencyCode);
            throw new DuplicateException();
        }
        catch (EntityNotFoundException)
        {
            // No duplicate found, continue
            return;
        }
        catch
        {
            throw;
        }
    }
}