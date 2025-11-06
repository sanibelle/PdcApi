using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Pdc.Application.DTOS;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.UseCases.ProgramOfStudy;

namespace Pdc.Application.UseCases;

public class CreateProgramOfStudy : ICreateProgramOfStudyUseCase
{
    private readonly IValidator<ProgramOfStudyDTO> _validator;
    private readonly IProgramOfStudyRepository _programOfStudyRepository;
    private readonly IMapper _mapper;

    public CreateProgramOfStudy(IProgramOfStudyRepository programOfStudyRepository, IMapper mapper, IValidator<ProgramOfStudyDTO> validator)
    {
        _programOfStudyRepository = programOfStudyRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ProgramOfStudyDTO> Execute(ProgramOfStudyDTO createProgramOfStudyDto)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(createProgramOfStudyDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        if (await _programOfStudyRepository.ExistsByCode(createProgramOfStudyDto.Code))
        {
            throw new DuplicateException("programOfStudyCodeExists");
        }

        ProgramOfStudy programOfStudy = _mapper.Map<ProgramOfStudy>(createProgramOfStudyDto);
        ProgramOfStudy savedProgramOfStudy = await _programOfStudyRepository.Add(programOfStudy);

        return _mapper.Map<ProgramOfStudyDTO>(savedProgramOfStudy);
    }
}