using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Version;
using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Application.UseCases.Version;

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
            throw new NotFoundException(nameof(AChangeable), changeableId);
        }
        ComplementaryInformation complementaryInformation = _mapper.Map<ComplementaryInformation>(complementaryInformationDTO);
        complementaryInformation.SetCreatedOnOnUntracked();
        complementaryInformation.SetCreatedByOnUntracked(currentUser);
        Guid versionId = await _complementaryInformationRepository.GetVersionByChangeableId(changeableId);
        ComplementaryInformation savedComplementaryInformation = await _complementaryInformationRepository.Add(complementaryInformation, versionId);
        return _mapper.Map<ComplementaryInformationDTO>(savedComplementaryInformation);
    }
}