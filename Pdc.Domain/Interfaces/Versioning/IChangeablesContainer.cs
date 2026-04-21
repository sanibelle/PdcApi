namespace Pdc.Domain.Interfaces.Versioning
{
    public interface IChangeablesContainer
    {
        void RemoveDeletedChangeables(List<Guid> changeableIdsToDelete);
    }
}
