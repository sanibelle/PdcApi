using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Version;
using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Application.UseCases;

public class UpdateComplementaryInformation : IUpdateComplementaryInformationUseCase
{
    private readonly IComplementaryInformationRepository _complementaryInformationRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ComplementaryInformationDTO> _validator;

    public UpdateComplementaryInformation(IComplementaryInformationRepository complementaryInformationRepository, IValidator<ComplementaryInformationDTO> validator, IMapper mapper)
    {
        _complementaryInformationRepository = complementaryInformationRepository;
        _validator=validator;
        _mapper = mapper;
    }

    public async Task<ComplementaryInformationDTO> Execute(ComplementaryInformationDTO complementaryInformationDTO, User currentUser)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(complementaryInformationDTO);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        if (!complementaryInformationDTO.Id.HasValue)
        {
            throw new InvalidDataException("missing id to update DTO");
        }
        Guid userId = await _complementaryInformationRepository.FindCreatedById(complementaryInformationDTO.Id.Value);
        if (currentUser.Id != userId && !currentUser.IsAdmin)
        {
            throw new UnauthorizedAccessException();
        }
        ComplementaryInformation complementaryInformation = _mapper.Map<ComplementaryInformation>(complementaryInformationDTO);
        complementaryInformation.ModifiedOn = DateTime.UtcNow;
        ComplementaryInformation savedComplementaryInformation = await _complementaryInformationRepository.Update(complementaryInformation);
        return _mapper.Map<ComplementaryInformationDTO>(savedComplementaryInformation);
    }
}