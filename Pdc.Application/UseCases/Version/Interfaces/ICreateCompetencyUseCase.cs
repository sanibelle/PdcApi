namespace Pdc.Application.UseCase;
using Pdc.Domain.Models.Versioning;

public interface IApproveVersion
{
    Task<ChangeRecord> Execute(Guid id);
}