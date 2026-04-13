using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Versioning;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Application.UseCases.Versioning;

public class UpdateChangeable(IChangeableRepository changeableRepository, IValidator<ChangeableDTO> validator, IMapper mapper) : IUpdateChangeableUseCase
{
    public async Task<ChangeableDTO> Execute(ChangeableDTO changeableDTO, Guid changeableId)
    {
        ValidationResult validationResult = await validator.ValidateAsync(changeableDTO);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        Changeable changeableToUpdate = await changeableRepository.FindById(changeableId);
        mapper.Map(changeableDTO, changeableToUpdate);
        Changeable updatedChangeable = await changeableRepository.Update(changeableToUpdate);
        return mapper.Map<ChangeableDTO>(updatedChangeable);
    }
}