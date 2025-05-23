﻿using Pdc.Infrastructure.Entities.Identity;

namespace Pdc.Infrastructure.Entities.Versioning;

public class ChangeableEntity
{
    private Guid _id;

    public required Guid Id
    {
        get => _id;
        set
        {
            if (Guid.Empty == value)
            {
                _id = Guid.NewGuid();
            }
            else
            {
                _id = value;
            }
        }
    }

    public required string Value { get; set; }
    public required ICollection<ComplementaryInformationEntity> ComplementaryInformations { get; set; }

    internal virtual void SetCreatedBy(IdentityUserEntity createdBy)
    {
        ComplementaryInformations.ToList().ForEach(x => x.SetCreatedBy(createdBy));
    }
}
