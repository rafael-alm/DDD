using projectName.domain.seedWork.entities.interfaces;

namespace projectName.domain.seedWork.aggregateHandler
{
    public interface IAggregateHandler
    {
        public bool Success { get; }
    }

    public interface IAggregateHandler<in TEntity> : IAggregateHandler
     where TEntity : IAggregateRoot<TEntity>, IEntity
    {
        void Execute(TEntity entity);
    }
}
