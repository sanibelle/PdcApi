using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Pdc.Application.DTOS;
using Pdc.Domain.Entities.CourseFramework;
using Pdc.Domain.Interfaces.Repositories;

namespace Pdc.Application.UseCase;

public class CreateProgramOfStudy : ICreateProgramOfStudyUseCase
{
    private readonly IValidator<CreateProgramOfStudyDTO> _validator;
    private readonly IProgramOfStudyRespository _programOfStudyRespository;
    private readonly IMapper _mapper;

    public CreateProgramOfStudy(IProgramOfStudyRespository programOfStudyRespository, IMapper mapper, IValidator<CreateProgramOfStudyDTO> validator)
    {
        _programOfStudyRespository = programOfStudyRespository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ProgramOfStudyDTO> Execute(CreateProgramOfStudyDTO createProgramOfStudyDto)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(createProgramOfStudyDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        ProgramOfStudy programOfStudy = _mapper.Map<ProgramOfStudy>(createProgramOfStudyDto);
        ProgramOfStudy savedProgramOfStudy = await _programOfStudyRespository.Add(programOfStudy);

        return _mapper.Map<ProgramOfStudyDTO>(savedProgramOfStudy);
    }
}