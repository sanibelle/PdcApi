using AutoMapper;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Versioning;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Application.UseCases.Versioning;

public class PublishChangeRecord(IChangeRecordRepository changeRecordRepository, IMapper mapper) : IPublishChangeRecordUseCase
{


    public async Task<ChangeRecordDTO> Execute(Guid changeRecordId)
    {
        ChangeRecord changeRecord = await changeRecordRepository.FindById(changeRecordId);
        if (changeRecord.IsDraft == false)
        {
            throw new InvalidOperationException("Only draft change records can be published.");
        }
        changeRecord = await changeRecordRepository.Publish(changeRecordId);
        return mapper.Map<ChangeRecordDTO>(changeRecord);
    }
}
