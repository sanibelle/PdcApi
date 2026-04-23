using AutoMapper;
using FluentValidation;
using Pdc.Application.DTOS;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.MinisterialSpecification;

namespace Pdc.Application.Services.Competency;

public class CompetencyService(IChangeDetailsRepository changeDetailsRepository,
                       IMapper mapper,
                       IValidator<CompetencyDTO> validator)
{
    public async Task<CompetencyDTO> RemoveDeletedChangeables(MinisterialCompetency competency)
    {
        List<Guid> changeableIdsToDelete = await changeDetailsRepository.FindDeletedChangeableIdByChangeRecordId(competency.ChangeRecord.Id!.Value);
        competency.RemoveDeletedChangeables(changeableIdsToDelete);

        return mapper.Map<CompetencyDTO>(competency);
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
}
