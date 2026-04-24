using AutoMapper;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Versioning;
using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Application.UseCases.Versioning;

public class PublishChangeRecord(IChangeRecordRepository changeRecordRepository, IMapper mapper) : IPublishChangeRecordUseCase
{


    public async Task<ChangeRecordDTO> Execute(Guid changeRecordId, User user)
    {
        ChangeRecord changeRecord = await changeRecordRepository.FindById(changeRecordId);
        if (changeRecord.IsDraft == false)
        {
            throw new InvalidOperationException("Only draft change records can be published.");
        }
        changeRecord.ValidatedBy = user;
        changeRecord.ValidatedOn = DateTime.UtcNow;
        changeRecord = await changeRecordRepository.Publish(changeRecord);
        return mapper.Map<ChangeRecordDTO>(changeRecord);
    }
}
