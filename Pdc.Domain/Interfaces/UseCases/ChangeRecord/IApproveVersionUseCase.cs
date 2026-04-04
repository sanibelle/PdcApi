namespace Pdc.Domain.Interfaces.UseCases.ChangeRecord;

using Pdc.Domain.Models.Versioning;

public interface IApproveVersionUseCase
{
    Task<ChangeRecord> Execute(Guid id);
}