using projectName.domain.seedWork.entities.interfaces;

namespace projectName.domain.seedWork.aggregateHandler
{
    internal interface IHandleCreateAggregater<TEntity> : IAggregateHandler
        where TEntity : IAggregateRoot<TEntity>, IEntity
    {
        public TEntity? Execute();
    }
}
