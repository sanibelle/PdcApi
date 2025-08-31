using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Pdc.Application.DTOS;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Application.UseCase;

public class CreateCompetency : ICreateCompetencyUseCase
{
    private readonly IValidator<CompetencyDTO> _validator;
    private readonly ICompetencyRepository _competencyRepository;
    private readonly IProgramOfStudyRespository _programOfStudyRepository;
    private readonly IMapper _mapper;

    public CreateCompetency(ICompetencyRepository competencyRepository,
                            IProgramOfStudyRespository programOfStudyRepository,
                            IMapper mapper,
                            IValidator<CompetencyDTO> validator)
    {
        _competencyRepository = competencyRepository;
        _programOfStudyRepository = programOfStudyRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<CompetencyDTO> Execute(string programOfStudyCode, CompetencyDTO createCompetencyDto, User currentUser)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(createCompetencyDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        await ThrowIfDuplicateCode(programOfStudyCode, createCompetencyDto.Code);
        ProgramOfStudy program = await _programOfStudyRepository.FindByCode(programOfStudyCode);
        MinisterialCompetency competency = _mapper.Map<MinisterialCompetency>(createCompetencyDto);
        competency.SetVersionOnUnversioned(new ChangeRecord());
        MinisterialCompetency savedCompetency = await _competencyRepository.Add(program, competency, currentUser);

        return _mapper.Map<CompetencyDTO>(savedCompetency);
    }

    private async Task ThrowIfDuplicateCode(string programOfStudyCode, string competencyCode)
    {
        if (await _competencyRepository.ExistsEntityByCode(programOfStudyCode, competencyCode))
        {
            throw new DuplicateException($"The competency with the code {competencyCode} attached to the program of study {programOfStudyCode} already exists.");
        }
    }
}