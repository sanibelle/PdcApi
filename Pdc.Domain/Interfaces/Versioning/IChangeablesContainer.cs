namespace Pdc.Domain.Interfaces.Versioning
{
    public interface IChangeablesContainer
    {
        void RemoveChangeablesByIds(List<Guid> changeableIdsToDelete);
        void SetValueById(Guid id, string value);
    }
}
