using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Pdc.Application.DTOS;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Competency;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Application.UseCases.Competency;

public class AddCompetency(ICompetencyRepository competencyRepository,
                        IProgramOfStudyRepository programOfStudyRepository,
                        IMapper mapper,
                        IValidator<CompetencyDTO> validator) : ICreateCompetencyUseCase
{
    private readonly IValidator<CompetencyDTO> _validator = validator;
    private readonly ICompetencyRepository _competencyRepository = competencyRepository;
    private readonly IProgramOfStudyRepository _programOfStudyRepository = programOfStudyRepository;
    private readonly IMapper _mapper = mapper;

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
        competency.SetVersionOnUntracked(new ChangeRecord(currentUser));
        competency.SetCreatedByOnUntracked(currentUser);
        competency.SetCreatedOnOnUntracked();
        MinisterialCompetency savedCompetency = await _competencyRepository.Add(program, competency);

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