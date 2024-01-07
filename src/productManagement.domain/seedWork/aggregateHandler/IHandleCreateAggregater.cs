using productManagement.domain.seedWork.entities.interfaces;

namespace productManagement.domain.seedWork.aggregateHandler
{
    internal interface IHandleCreateAggregater<TEntity> : IAggregateHandler
        where TEntity : IAggregateRoot<TEntity>, IEntity
    {
        public TEntity? Execute();
    }
}
