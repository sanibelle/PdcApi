using Pdc.Domain.Interfaces.Repositories;

namespace Pdc.Application.UseCases.Versioning;

public class PublishVersion(IComplementaryInformationRepository complementaryInformationRepository, Guid versionId)
{
    //private readonly IComplementaryInformationRepository _complementaryInformationRepository = complementaryInformationRepository;
    //private readonly IMapper _mapper = mapper;
    //private readonly IValidator<ComplementaryInformationDTO> _validator = validator;

    //public async Task<ComplementaryInformationDTO> Execute(ComplementaryInformationDTO complementaryInformationDTO, Guid changeableId, User currentUser)
    //{
    //    ValidationResult validationResult = await _validator.ValidateAsync(complementaryInformationDTO);
    //    if (!validationResult.IsValid)
    //    {
    //        throw new ValidationException(validationResult.Errors);
    //    }
    //    if (!await _complementaryInformationRepository.ChangeableExists(changeableId))
    //    {
    //        throw new NotFoundException(nameof(AChangeable), changeableId);
    //    }
    //    ComplementaryInformation complementaryInformation = _mapper.Map<ComplementaryInformation>(complementaryInformationDTO);
    //    complementaryInformation.SetCreatedOnOnUntracked();
    //    complementaryInformation.SetCreatedByOnUntracked(currentUser);
    //    Guid versionId = await _complementaryInformationRepository.GetChangeRecordByChangeableId(changeableId);
    //    ComplementaryInformation savedComplementaryInformation = await _complementaryInformationRepository.Add(complementaryInformation, versionId, changeableId);
    //    return _mapper.Map<ComplementaryInformationDTO>(savedComplementaryInformation);
}
