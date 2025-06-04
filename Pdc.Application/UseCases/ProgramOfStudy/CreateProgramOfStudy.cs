using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Pdc.Application.DTOS;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.CourseFramework;

namespace Pdc.Application.UseCase;

public class CreateProgramOfStudy : ICreateProgramOfStudyUseCase
{
    private readonly IValidator<ProgramOfStudyDTO> _validator;
    private readonly IProgramOfStudyRespository _programOfStudyRespository;
    private readonly IMapper _mapper;

    public CreateProgramOfStudy(IProgramOfStudyRespository programOfStudyRespository, IMapper mapper, IValidator<ProgramOfStudyDTO> validator)
    {
        _programOfStudyRespository = programOfStudyRespository;
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
        if (await _programOfStudyRespository.ExistsByCode(createProgramOfStudyDto.Code))
        {
            throw new DuplicateException("programOfStudyCodeExists");
        }

        ProgramOfStudy programOfStudy = _mapper.Map<ProgramOfStudy>(createProgramOfStudyDto);
        ProgramOfStudy savedProgramOfStudy = await _programOfStudyRespository.Add(programOfStudy);

        return _mapper.Map<ProgramOfStudyDTO>(savedProgramOfStudy);
    }
}