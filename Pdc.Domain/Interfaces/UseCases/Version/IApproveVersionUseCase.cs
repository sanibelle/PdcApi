namespace Pdc.Domain.Interfaces.UseCases.Version;

using Pdc.Domain.Models.Versioning;

public interface IApproveVersionUseCase
{
    Task<ChangeRecord> Execute(Guid id);
}