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
    private readonly IVersionRepository _versionRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ComplementaryInformationDTO> _validator;

    public UpdateComplementaryInformation(IComplementaryInformationRepository complementaryInformationRepository, IVersionRepository versionRepository, IValidator<ComplementaryInformationDTO> validator, IMapper mapper)
    {
        _complementaryInformationRepository = complementaryInformationRepository;
        _versionRepository=versionRepository;
        _validator=validator;
        _mapper = mapper;
    }

    public async Task<ComplementaryInformationDTO> Execute(ComplementaryInformationDTO complementaryInformationDTO, User currentUser, Guid complementaryInformationId)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(complementaryInformationDTO);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        Guid userId = await _complementaryInformationRepository.FindCreatedById(complementaryInformationId);
        if (currentUser.Id != userId && !currentUser.IsAdmin)
        {
            throw new UnauthorizedAccessException();
        }
        ComplementaryInformation existingComplementaryInformation = await _complementaryInformationRepository.FindById(complementaryInformationId);
        Guid versionId = await _versionRepository.FindParentByVersionId(existingComplementaryInformation.WrittenOnVersion!.Id!.Value);
        ComplementaryInformation complementaryInformation = _mapper.Map<ComplementaryInformation>(complementaryInformationDTO);
        complementaryInformation.Id = complementaryInformationId;
        complementaryInformation.ModifiedOn = DateTime.UtcNow;
        ComplementaryInformation savedComplementaryInformation = await _complementaryInformationRepository.Update(complementaryInformation, versionId);
        return _mapper.Map<ComplementaryInformationDTO>(savedComplementaryInformation);
    }
}