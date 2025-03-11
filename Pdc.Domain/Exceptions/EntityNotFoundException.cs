namespace Pdc.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName, object id)
            : base($"{entityName} with id {id} was not found.")
        {
            EntityName = entityName;
            Id = id;
        }

        public string EntityName { get; }
        public object Id { get; }
    }
}