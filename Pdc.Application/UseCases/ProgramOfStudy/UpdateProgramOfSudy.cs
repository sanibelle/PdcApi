using AutoMapper;
using FluentValidation;
using Pdc.Application.DTOS;
using Pdc.Domain.Entities.CourseFramework;
using Pdc.Domain.Interfaces.Repositories;

namespace Pdc.Application.UseCase;

public class UpdateProgramOfSudy : IUpdateProgramOfStudyUseCase
{
    private readonly IValidator<UpsertProgramOfStudyDTO> _validator;
    private readonly IProgramOfStudyRespository _programOfStudyRespository;
    private readonly IMapper _mapper;

    public UpdateProgramOfSudy(IProgramOfStudyRespository programOfStudyRespository,
                               IMapper mapper,
                               IValidator<UpsertProgramOfStudyDTO> validator)
    {
        _programOfStudyRespository = programOfStudyRespository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ProgramOfStudyDTO> Execute(Guid id, UpsertProgramOfStudyDTO updateProgramOfStudyDto)
    {
        var validationResult = await _validator.ValidateAsync(updateProgramOfStudyDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        ProgramOfStudy existingProgramOfStudy = await _programOfStudyRespository.FindById(id);
        _mapper.Map(updateProgramOfStudyDto, existingProgramOfStudy);
        ProgramOfStudy updatedProgramOfStudy = await _programOfStudyRespository.Update(existingProgramOfStudy);
        return _mapper.Map<ProgramOfStudyDTO>(updatedProgramOfStudy);
    }
}