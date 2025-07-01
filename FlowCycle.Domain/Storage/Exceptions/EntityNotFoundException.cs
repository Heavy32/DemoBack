namespace FlowCycle.Domain.Storage.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string EntityType { get; }
        public string EntityName { get; }

        public EntityNotFoundException(string entityType, string entityName)
            : base($"Entity of type '{entityType}' with name '{entityName}' was not found.")
        {
            EntityType = entityType;
            EntityName = entityName;
        }
    }
} 