using AutoMapper;
using Pdc.Application.DTOS;
using Pdc.Application.Services.Competency;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Competency;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Application.UseCases;

public class GetCompetencyWithChangeDetails(ICompetencyRepository competencyRepository, IChangeRecordRepository changeRecordRepository, IChangeDetailsRepository changeDetailsRepository, CompetencyService competencyService, IMapper mapper) : IGetCompetencyWithChangeDetailsUseCase
{
    public async Task<CompetencyDTO> Execute(string programOfStudyCode, string competencyCode, int changeRecordNumber)
    {
        MinisterialCompetency competency = await competencyRepository.FindByCode(competencyCode);
        if (!competency.ChangeRecord.Id.HasValue)
        {
            throw new NullReferenceException("Competency must have a valid ChangeRecord with an Id.");
        }
        Guid parentChangeRecordId = await changeRecordRepository.FindIdByParentIdAndNumber(changeRecordNumber, competency.ChangeRecord.Id.Value);


        // removing the changeables that have been deleted in the change record history, so that they are not included in the response
        // but keeping the delete of the current version since the change details will need them
        CompetencyDTO competencyDTO = await competencyService.RemoveDeletedChangeables(competency, competency.ChangeRecord.Id.Value, changeRecordNumber);
        List<ChangeDetail> changeDetails = await changeDetailsRepository.GetChangeDetailsByChangeRecordId(parentChangeRecordId);
        competencyDTO.ChangeDetails = mapper.Map<List<ChangeDetailDTO>>(changeDetails);
        return competencyDTO;
    }
}
