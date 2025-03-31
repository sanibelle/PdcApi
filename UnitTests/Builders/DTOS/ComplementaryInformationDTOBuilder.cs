using Pdc.Application.DTOS.Common;

namespace Pdc.Tests.Builders.DTOS;

public class ComplementaryInformationDTOBuilder
{
    private Guid _id = Guid.NewGuid();

    public ComplementaryInformationDTOBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public ComplementaryInformationDTO Build()
    {
        return new ComplementaryInformationDTO
        {
            Id = _id
        };
    }
}
