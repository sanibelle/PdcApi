using AutoMapper;
using FluentValidation;
using Pdc.Application.DTOS;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Competency;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Application.UseCases;

public class UpdatePublishedCompetency(ICompetencyRepository competencyRepository,
                           IChangeDetailsRepository changeDetailsRepository,
                           IMapper mapper,
                           IValidator<CompetencyDTO> validator) : IUpdatePublishedCompetencyUseCase
{
    public async Task<CompetencyDTO> Execute(string programOfStudyCode, string competencyCode, CompetencyDTO updateCompetencyDto, User currentUser)
    {
        var validationResult = await validator.ValidateAsync(updateCompetencyDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        if (competencyCode != updateCompetencyDto.Code)
        {
            throw new ValidationException("Competency code in the URL does not match the code in the body. The code should not be changed.");
        }
        MinisterialCompetency competencyToUpdate = await competencyRepository.FindByCode(updateCompetencyDto.Code);
        if (competencyToUpdate.ChangeRecord == null)
        {
            throw new Exception("Missing changeRecord on a competency to update.");
        }
        if (!competencyToUpdate.IsLatestVersion() || competencyToUpdate.ChangeRecord.Id != updateCompetencyDto.ChangeRecordId)
        {
            throw new InvalidOperationException("The targetted change record to update is not the latest.");
        }
        ChangeRecord changeRecord = new ChangeRecord(competencyToUpdate.ChangeRecord, currentUser);
        mapper.Map(updateCompetencyDto, competencyToUpdate);

        // setting the new change record for the new draft version
        competencyToUpdate.ChangeRecord = changeRecord;
        competencyToUpdate.SetChangeRecordOnUntracked(changeRecord);
        competencyToUpdate.SetCreatedByOnUntracked(currentUser);
        competencyToUpdate.SetCreatedOnOnUntracked();


        // UpdateAndTrack se charge de gérer le suivi des changements
        MinisterialCompetency updatedCompetency = await competencyRepository.UpdateAndTrack(competencyToUpdate);

        // Removing the deleted elements.
        List<Guid> changeableIdsToDelete = await changeDetailsRepository.FindDeletedChangeableIdByChangeRecordId(updatedCompetency.ChangeRecord.Id!.Value);
        updatedCompetency.RemoveDeletedChangeables(changeableIdsToDelete);

        return mapper.Map<CompetencyDTO>(updatedCompetency);
        // TODO faire fonctionner quand on met à jour une version draft, il faudrait modifier les changeDetails quand on CRUD.
    }
}