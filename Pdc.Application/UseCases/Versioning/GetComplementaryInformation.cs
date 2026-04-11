using AutoMapper;
using Pdc.Domain.DTOS.Common;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.Versioning;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Application.UseCases.Versioning;

public class GetComplementaryInformation : IGetComplementaryInformationUseCase
{
    private readonly IComplementaryInformationRepository _complementaryInformationRepository;
    private readonly IMapper _mapper;

    public GetComplementaryInformation(IComplementaryInformationRepository complementaryInformationRepository,
        IMapper mapper)
    {
        _complementaryInformationRepository = complementaryInformationRepository;
        _mapper = mapper;
    }

    public async Task<ComplementaryInformationDTO> Execute(Guid id)
    {
        ComplementaryInformation complementaryInformation = await _complementaryInformationRepository.FindById(id);
        return _mapper.Map<ComplementaryInformationDTO>(complementaryInformation);
    }
}