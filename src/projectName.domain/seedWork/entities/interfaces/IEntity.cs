namespace projectName.domain.seedWork.entities.interfaces
{
    public interface IEntity<TId> : IEntity where TId: struct
    {
        public TId Id { get; init; }
    }

    public interface IEntity
    {
    }
}
