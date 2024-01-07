using productManagement.domain.seedWork.entities.interfaces;
using productManagement.domain.shared.seedWork.notification;

namespace productManagement.domain.seedWork.aggregateHandler
{
    public abstract class HandlerCreateAggregate<TEntity> : IHandleCreateAggregater<TEntity>
    where TEntity : IAggregateRoot<TEntity>, IEntity
    {
        protected readonly INotification notification;

        protected HandlerCreateAggregate(INotification notification)
            => this.notification = notification;

        public INotification Notification => notification;
        bool IAggregateHandler.Success => !notification.HasError;

        protected abstract TEntity? execute();

        public TEntity? Execute()
            => execute();
    }
}
    