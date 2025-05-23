﻿using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Infrastructure.Exceptions;

namespace Pdc.Application.UseCase;

public class DeleteCompetency : IDeleteCompetencyUseCase
{
    private readonly ICompetencyRespository _programOfStudyRespository;

    public DeleteCompetency(ICompetencyRespository programOfStudyRespository)
    {
        _programOfStudyRespository = programOfStudyRespository;
    }

    public async Task Execute(string programOfStudyCode, string competencyCode)
    {
        try
        {
            await _programOfStudyRespository.FindByCode(programOfStudyCode, competencyCode);
        }
        catch (EntityNotFoundException)
        {
            throw new NotFoundException();
        }
        await _programOfStudyRespository.Delete(programOfStudyCode, competencyCode);
    }
}