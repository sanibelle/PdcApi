using FluentValidation;
using Pdc.Application.DTOS;
using Pdc.Domain.Enums;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Application.Services.Competency;

public class CompetencyService(IChangeDetailsRepository changeDetailsRepository,
                        IChangeRecordRepository changeRecordRepository,
                        IValidator<CompetencyDTO> validator)
{
    public async Task<MinisterialCompetency> RemoveDeletedChangeables(MinisterialCompetency competency, Guid targetChangeRecordId, int? recordNumberToSkip = null)
    {
        List<Guid> changeableIdsToDelete = await changeDetailsRepository.FindDeletedChangeableIdByChangeRecordId(targetChangeRecordId, recordNumberToSkip);
        competency.RemoveChangeablesByIds(changeableIdsToDelete);
        return competency;
    }

    public async Task ValidateCompetencyAsync(string competencyCode, CompetencyDTO updateCompetencyDto)
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
    }


    /// <summary>
    /// Update the competency uppdated changeable values with the closest previous version.
    /// Since the changebles keep the latest value, if we need to check the diff between v3 and v4, we need to get the value of the changeables in v4, not the value of v5.
    /// </summary>
    /// <param name="changeDetails">The list of changeDetails to look for</param>
    /// <param name="competency">The competency updated</param>
    /// <param name="targetChangeRecordId">The targetted changeRecordId</param>
    public async Task SetChangeableValueOnTargetVersion(List<ChangeDetail> changeDetails, MinisterialCompetency competency, Guid targetChangeRecordId)
    {
        List<ChangeDetail> createUpdateChangeDetails = changeDetails.Where(x => x.ChangeType != ChangeType.Delete).ToList();
        if (createUpdateChangeDetails.Count == 0) return;
        await foreach (var changeDetail in changeRecordRepository.FindNextChangeDetailsByChangeType(createUpdateChangeDetails, ChangeType.Update, competency.ChangeRecord.RootId!.Value))
        {
            competency.SetValueById(changeDetail.Changeable.Id!.Value, changeDetail.OldValue ?? "");
        }
    }

    public async Task RemoveAddedChangeablesFromNextVersions(MinisterialCompetency competency, int changeRecordNumber)
    {
        List<Guid> changeDetailIds = await changeRecordRepository.FindNextChangeDetailsByChangeRecordNumber(changeRecordNumber, ChangeType.Add, competency.ChangeRecord.RootId!.Value);
        competency.RemoveChangeablesByIds(changeDetailIds);
    }
}
