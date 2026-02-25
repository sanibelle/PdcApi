using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Version;
using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Application.UseCases;

public class AddComplementaryInformation : IAddComplementaryInformationUseCase
{
    private readonly IComplementaryInformationRepository _complementaryInformationRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ComplementaryInformationDTO> _validator;

    public AddComplementaryInformation(IComplementaryInformationRepository complementaryInformationRepository, IValidator<ComplementaryInformationDTO> validator, IMapper mapper)
    {
        _complementaryInformationRepository = complementaryInformationRepository;
        _validator=validator;
        _mapper = mapper;
    }

    public async Task<ComplementaryInformationDTO> Execute(ComplementaryInformationDTO complementaryInformationDTO, Guid changeableId, User currentUser)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(complementaryInformationDTO);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        if (!await _complementaryInformationRepository.ChangeableExists(changeableId))
        {
            throw new ArgumentException($"Changeable with id {changeableId} does not exist.");
        }
        ComplementaryInformation complementaryInformation = _mapper.Map<ComplementaryInformation>(complementaryInformationDTO);
        complementaryInformation.CreatedOn = DateTime.Now;
        complementaryInformation.SetCreatedByOnUntracked(currentUser);
        complementaryInformation.SetVersionOnUntracked(await _complementaryInformationRepository.GetVersionByChangeableId(changeableId));
        ComplementaryInformation savedComplementaryInformation = await _complementaryInformationRepository.Add(complementaryInformation, changeableId);
        return _mapper.Map<ComplementaryInformationDTO>(savedComplementaryInformation);
    }
}