using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Versioning;
using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Application.UseCases.Versioning;

public class AddComplementaryInformation(IComplementaryInformationRepository complementaryInformationRepository, IValidator<ComplementaryInformationDTO> validator, IMapper mapper) : IAddComplementaryInformationUseCase
{
    private readonly IComplementaryInformationRepository _complementaryInformationRepository = complementaryInformationRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<ComplementaryInformationDTO> _validator = validator;

    public async Task<ComplementaryInformationDTO> Execute(ComplementaryInformationDTO complementaryInformationDTO, Guid changeableId, User currentUser)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(complementaryInformationDTO);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        if (!await _complementaryInformationRepository.ChangeableExists(changeableId))
        {
            throw new NotFoundException(nameof(Changeable), changeableId);
        }
        ComplementaryInformation complementaryInformation = _mapper.Map<ComplementaryInformation>(complementaryInformationDTO);
        complementaryInformation.SetCreatedOnOnUntracked();
        complementaryInformation.SetCreatedByOnUntracked(currentUser);
        Guid versionId = await _complementaryInformationRepository.GetChangeRecordByChangeableId(changeableId);
        ComplementaryInformation savedComplementaryInformation = await _complementaryInformationRepository.Add(complementaryInformation, versionId, changeableId);
        return _mapper.Map<ComplementaryInformationDTO>(savedComplementaryInformation);
    }
}