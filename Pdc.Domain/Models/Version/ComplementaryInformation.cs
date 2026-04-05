using Pdc.Domain.Interfaces.Propagables;
using Pdc.Domain.Models.Security;

namespace Pdc.Domain.Models.Versioning;

public class ComplementaryInformation : IChangeRecordPropagable, ICreatedByPropagable, ICreatedOnPropagable
{
    public Guid? Id { get; set; }
    /// <summary>
    /// Version à laquelle l'information a été ajoutée
    /// </summary>
    public ChangeRecord? WrittenOnChangeRecord { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public DateTime CreatedOn { get; set; }
    public required string Text { get; set; }
    public required User CreatedBy { get; set; }
    public void SetChangeRecordOnUntracked(ChangeRecord version)
    {
        WrittenOnChangeRecord ??= version;
    }

    public void SetCreatedByOnUntracked(User user)
    {
        CreatedBy ??= user;
    }

    public void SetCreatedOnOnUntracked()
    {
        if (CreatedOn == default)
        {
            CreatedOn = DateTime.UtcNow;
            ModifiedOn = null;
        }
    }

    public ComplementaryInformation()
    {
        Id = Guid.NewGuid();
        CreatedOn = DateTime.UtcNow;
    }
}
