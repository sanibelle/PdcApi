using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Application.Services.Competency;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Competency;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Application.UseCases;

public class UpdatePublishedCompetency(ICompetencyRepository competencyRepository,
                           IMapper mapper,
                           CompetencyService competencyService) : IUpdatePublishedCompetencyUseCase
{
    public async Task<CompetencyDTO> Execute(string programOfStudyCode, string competencyCode, CompetencyDTO updateCompetencyDto, User currentUser)
    {
        await competencyService.ValidateCompetencyAsync(competencyCode, updateCompetencyDto);
        MinisterialCompetency competencyToUpdate = await competencyRepository.FindByCode(updateCompetencyDto.Code);
        if (competencyToUpdate.ChangeRecord == null)
        {
            throw new InvalidChangeRecordException("Missing changeRecord on a competency to update.");
        }
        if (competencyToUpdate.IsLatestVersion() || competencyToUpdate.ChangeRecord.Id != updateCompetencyDto.ChangeRecordId || competencyToUpdate.ChangeRecord.ChangeRecordNumber != updateCompetencyDto.ChangeRecordNumber)
        {
            throw new InvalidChangeRecordException("The targeted change record to update is not the latest.");
        }
        ChangeRecord changeRecord = new ChangeRecord(competencyToUpdate.ChangeRecord, currentUser);
        mapper.Map(updateCompetencyDto, competencyToUpdate);

        // setting the new change record for the new draft version
        competencyToUpdate.ChangeRecord = changeRecord;
        competencyToUpdate.SetChangeRecordOnUntracked(changeRecord);
        competencyToUpdate.SetCreatedByOnUntracked(currentUser);
        competencyToUpdate.SetCreatedOnOnUntracked();


        // UpdateAndTrack se charge de gérer le suivi des changements
        MinisterialCompetency updatedCompetency = await competencyRepository.UpdateWithChangeTracking(competencyToUpdate);
        if (!updatedCompetency.ChangeRecord.Id.HasValue)
        {
            throw new NullReferenceException("Competency must have a valid ChangeRecord with an Id.");
        }
        return await competencyService.RemoveDeletedChangeables(updatedCompetency, updatedCompetency.ChangeRecord.Id.Value);
    }
}