using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.ChangeRecord;
using Pdc.Domain.Models.Versioning;
using Pdc.Domain.Models.Security;

namespace Pdc.Application.UseCases.Versioning;

public class UpdateComplementaryInformation(IComplementaryInformationRepository complementaryInformationRepository, IChangeRecordRepository versionRepository, IValidator<ComplementaryInformationDTO> validator, IMapper mapper) : IUpdateComplementaryInformationUseCase
{
    private readonly IComplementaryInformationRepository _complementaryInformationRepository = complementaryInformationRepository;
    private readonly IChangeRecordRepository _versionRepository = versionRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<ComplementaryInformationDTO> _validator = validator;

    public async Task<ComplementaryInformationDTO> Execute(ComplementaryInformationDTO complementaryInformationDTO, User currentUser, Guid complementaryInformationId)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(complementaryInformationDTO);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        Guid authorId = await _complementaryInformationRepository.FindCreatedByByComplementaryInformationId(complementaryInformationId);
        if (currentUser.Id != authorId && !currentUser.IsAdmin)
        {
            throw new UnauthorizedAccessException();
        }
        ComplementaryInformation existingComplementaryInformation = await _complementaryInformationRepository.FindById(complementaryInformationId);
        if (existingComplementaryInformation.WrittenOnChangeRecord?.Id == null)
        {
            throw new InvalidOperationException("WrittenOnVersion is not set on the existing entity.");
        }
        Guid versionId = await _versionRepository.FindParentByChangeRecordId(existingComplementaryInformation.WrittenOnChangeRecord!.Id!.Value);
        ComplementaryInformation complementaryInformation = _mapper.Map<ComplementaryInformation>(complementaryInformationDTO);
        complementaryInformation.Id = complementaryInformationId;
        complementaryInformation.ModifiedOn = DateTime.UtcNow;
        // To prevent altering existing CreatedOn and CreatedBy values, we set them to the existing values before updating
        complementaryInformation.CreatedOn = existingComplementaryInformation.CreatedOn;
        complementaryInformation.CreatedBy = existingComplementaryInformation.CreatedBy;
        ComplementaryInformation savedComplementaryInformation = await _complementaryInformationRepository.Update(complementaryInformation, versionId);
        return _mapper.Map<ComplementaryInformationDTO>(savedComplementaryInformation);
    }
}