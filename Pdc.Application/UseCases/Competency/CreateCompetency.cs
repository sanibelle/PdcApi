using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Pdc.Application.DTOS;
using Pdc.Application.Exceptions;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Versioning;

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
        MinisterialCompetencyEntity competency = _mapper.Map<MinisterialCompetencyEntity>(createCompetencyDto);
        competency.SetVersion(new ChangeRecord());
        MinisterialCompetencyEntity savedCompetency = await _competencyRepository.Add(program, competency);

        return _mapper.Map<CompetencyDTO>(savedCompetency);
    }

    private async Task ThrowIfDuplicateCode(string programOfStudyCode, string competencyCode)
    {
        try
        {
            MinisterialCompetencyEntity comptency = await _competencyRepository.FindByCode(programOfStudyCode, competencyCode);
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