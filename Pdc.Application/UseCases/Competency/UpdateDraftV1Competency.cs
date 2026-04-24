using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Application.Services.Competency;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Competency;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Security;


namespace Pdc.Application.UseCases;

public class UpdateDraftV1Competency(ICompetencyRepository competencyRepository,
                           IMapper mapper,
                           CompetencyService competencyService) : IUpdateDraftV1CompetencyUseCase
{
    public async Task<CompetencyDTO> Execute(string programOfStudyCode, string competencyCode, CompetencyDTO updateCompetencyDto, User currentUser)
    {
        await competencyService.ValidateCompetencyAsync(competencyCode, updateCompetencyDto);
        MinisterialCompetency competencyToUpdate = await competencyRepository.FindByCode(updateCompetencyDto.Code);
        if (!competencyToUpdate.IsDraftAndV1OrNull())
        {
            throw new InvalidChangeRecordException("Cannot update a non-draft competency with change record greater than 1.");
        }
        mapper.Map(updateCompetencyDto, competencyToUpdate);
        // On prend la version actuelle et on l'assigne à tous les objets qui ont une version.
        competencyToUpdate.SetChangeRecordOnUntracked(competencyToUpdate.ChangeRecord!);
        competencyToUpdate.SetCreatedByOnUntracked(currentUser);
        competencyToUpdate.SetCreatedOnOnUntracked();
        MinisterialCompetency updatedCompetency = await competencyRepository.Update(competencyToUpdate);
        return mapper.Map<CompetencyDTO>(updatedCompetency);
    }
}