namespace Pdc.Domain.Interfaces.UseCases.Versioning;

using Pdc.Domain.DTOS.Common;

public interface IUpdateChangeableUseCase
{
    Task<ChangeableDTO> Execute(ChangeableDTO changeable, Guid changeableId);
}