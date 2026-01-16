namespace Pdc.Domain.Interfaces.UseCases.Version;
using Pdc.Domain.Models.Versioning;

public interface IApproveVersion
{
    Task<ChangeRecord> Execute(Guid id);
}