namespace Pdc.Application.UseCases;
using Pdc.Domain.Models.Versioning;

public interface IApproveVersion
{
    Task<ChangeRecord> Execute(Guid id);
}