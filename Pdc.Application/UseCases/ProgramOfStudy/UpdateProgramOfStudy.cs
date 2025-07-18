﻿using AutoMapper;
using FluentValidation;
using Pdc.Application.DTOS;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.CourseFramework;

namespace Pdc.Application.UseCase;

public class UpdateProgramOfStudy : IUpdateProgramOfStudyUseCase
{
    private readonly IValidator<ProgramOfStudyDTO> _validator;
    private readonly IProgramOfStudyRespository _programOfStudyRespository;
    private readonly IMapper _mapper;

    public UpdateProgramOfStudy(IProgramOfStudyRespository programOfStudyRespository,
                               IMapper mapper,
                               IValidator<ProgramOfStudyDTO> validator)
    {
        _programOfStudyRespository = programOfStudyRespository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ProgramOfStudyDTO> Execute(string code, ProgramOfStudyDTO updateProgramOfStudyDto)
    {
        var validationResult = await _validator.ValidateAsync(updateProgramOfStudyDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        if (updateProgramOfStudyDto.Code != code && await _programOfStudyRespository.ExistsByCode(updateProgramOfStudyDto.Code))
        {
            throw new DuplicateException("programOfStudyCodeExists");
        }
        ProgramOfStudy existingProgramOfStudy = await _programOfStudyRespository.FindByCode(code);
        _mapper.Map(updateProgramOfStudyDto, existingProgramOfStudy);
        ProgramOfStudy updatedProgramOfStudy = await _programOfStudyRespository.Update(existingProgramOfStudy);
        return _mapper.Map<ProgramOfStudyDTO>(updatedProgramOfStudy);
    }
}