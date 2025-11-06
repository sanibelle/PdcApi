namespace Pdc.Domain.UseCases.Version;
using Pdc.Domain.Models.Versioning;

public interface IApproveVersion
{
    Task<ChangeRecord> Execute(Guid id);
}