using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Versioning;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Application.UseCases.Versioning;

public class UpdateChangeable(IChangeableRepository changeableRepository, IValidator<ChangeableDTO<string>> validator, IMapper mapper) : IUpdateChangeableUseCase
{
    public async Task<ChangeableDTO<string>> Execute(ChangeableDTO<string> changeableDTO, Guid changeableId)
    {
        ValidationResult validationResult = await validator.ValidateAsync(changeableDTO);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        Changeable changeableToUpdate = await changeableRepository.FindById(changeableId);
        mapper.Map(changeableDTO, changeableToUpdate);
        changeableToUpdate.Id = changeableId;
        Changeable updatedChangeable = await changeableRepository.Update(changeableToUpdate);
        return mapper.Map<ChangeableDTO<string>>(updatedChangeable);
    }
}