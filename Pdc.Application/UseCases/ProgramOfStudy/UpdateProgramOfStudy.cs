using AutoMapper;
using FluentValidation;
using Pdc.Application.DTOS;
using Pdc.Domain.Entities.CourseFramework;
using Pdc.Domain.Interfaces.Repositories;

namespace Pdc.Application.UseCase;

public class UpdateProgramOfStudy : IUpdateProgramOfStudyUseCase
{
    private readonly IValidator<CreateProgramOfStudyDTO> _validator;
    private readonly IProgramOfStudyRespository _programOfStudyRespository;
    private readonly IMapper _mapper;

    public UpdateProgramOfStudy(IProgramOfStudyRespository programOfStudyRespository,
                               IMapper mapper,
                               IValidator<CreateProgramOfStudyDTO> validator)
    {
        _programOfStudyRespository = programOfStudyRespository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ProgramOfStudyDTO> Execute(string code, CreateProgramOfStudyDTO updateProgramOfStudyDto)
    {
        var validationResult = await _validator.ValidateAsync(updateProgramOfStudyDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        ProgramOfStudy existingProgramOfStudy = await _programOfStudyRespository.FindById(code);
        _mapper.Map(updateProgramOfStudyDto, existingProgramOfStudy);
        ProgramOfStudy updatedProgramOfStudy = await _programOfStudyRespository.Update(existingProgramOfStudy);
        return _mapper.Map<ProgramOfStudyDTO>(updatedProgramOfStudy);
    }
}