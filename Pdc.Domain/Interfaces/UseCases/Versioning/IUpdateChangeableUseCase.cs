namespace Pdc.Domain.Interfaces.UseCases.Versioning;

using Pdc.Domain.DTOS.Common;

public interface IUpdateChangeableUseCase
{
    Task<ChangeableDTO<string>> Execute(ChangeableDTO<string> changeable, Guid changeableId);
}